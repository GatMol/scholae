const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");
const multer  = require('multer');
const upload = multer({ dest: 'uploads/'});
const fs = require('fs');
const util = require('util');
const unlinkFile = util.promisify(fs.unlink);
const { uploadFile, getFileStream, s3 } = require('../s3');
const http = require('http');


const location = "/libro/immagine/";

var toArray = require('stream-to-array');

BigInt.prototype.toJSON = function() {       
    return this.toString()
}

router.post("/", async (req, res, next)=> {
    //TODO: nella richiesta aggiungere un campo libroimage con il file (stream)
    const newLibro = {
        ISBN: req.body.ISBN,
        Nome: req.body.Nome,
        Autore: req.body.Autore,
        Edizione: req.body.Edizione,
        Editore: req.body.Editore,
        Prezzo: req.body.Prezzo,
        Materia_id: req.body.Materia_id,
        Utente_id: parseInt(req.body.Utente_id),
    }

    await prisma.libro.create({data: newLibro})
    .then(result => {
        console.log(result);
        console.log(s3);
        res.status(201).json(result);
    })
    .catch(err => {
        console.log(err);
        res.status(500).json({
            error: err
        });
    });
});

router.post("/foto/:id", upload.single('libroImage'), async (req, res, next)=> {
    const file = req.file;
    console.log(file);
    const result = await uploadFile(file);
    await unlinkFile(file.path);
    console.log(result);
    await prisma.libro.update({
        where: {
            Id: parseInt(req.params.id)
          },
          data: {
            Immagine: location +  file.filename
          }
    })
    .then(result => {
        console.log(location +  file.filename);
        const data = location +  file.filename;
        console.log(data);
        res.status(201).send(data);
        console.log(res);
    })
    .catch(err => {
        console.log(err);
        res.status(500).json({
            error: err
        });
    });
});

router.get("/:utenteId", async (req, res, next) => {
   await prisma.libro.findMany({
       select: {
        Id: true,
        Nome: true,
        ISBN: true,
        Autore: true,
        Editore: true,
        Edizione: true,
        Prezzo: true,
        Materia: true,
        Utente:{
            select:{
                Nome: true,
                Cognome: true
            }
        },
        Immagine: true
       },
       where: {
           NOT: {
            Utente_id: parseInt(req.params.utenteId)
           }
       }
   }).then(result => {
        res.status(200).json(result);
    })
    .catch(err => {
        res.status(500).json({
            error: err
        });
    });
});

router.get("/cercaPerNome/:nome", async (req, res, next) => {
    const nome = req.params.nome + '%';
    const result = await prisma.libro.findMany({
        select: {
            Id: true,
            Nome: true,
            ISBN: true,
            Autore: true,
            Editore: true,
            Edizione: true,
            Prezzo: true,
            Materia: true,
            Utente:{
                select:{
                    Nome: true,
                    Cognome: true
                }
            },
            Immagine: true
        },
        where: {
            OR: [

            {
                Nome: {
                startsWith: nome
                }
            },
            {
                Materia_id: {
                    startsWith: nome
                }
            }  
        ],
        NOT: {
            Utente_id: parseInt(req.body.Utente_id)
        }
        }
    })
    .catch((err) => {
        return res.status(500).json({
            error: err
        });
    });
    res.status(200).json(result);
});

//*************************************************************A */

function getImmagineLibro(key) {
    const readStream = getFileStream(key);
    console.log(readStream);
    return readStream;
};

const bucketName = process.env.S3_BUCKET;

router.get("/immagine/:filename", (req, res) => {
    const downloadParams = {
        Key: req.params.filename,
        Bucket: bucketName,
        ResponseContentType: "image/jpeg"
    };
    s3.getObject(downloadParams, function(err, data) {
        res.writeHead(200, {'Content-Type': 'image/jpeg'});
        res.write(data.Body, 'binary');
        res.end(null, 'binary');
    });
});


router.get("/cercaPerId/:libroId", async (req, res) => {
    await db.libro.findUnique({
        where: {
            Id: parseInt(req.params.libroId)
        },
        select: {
            Id: true,
            Nome: true,
            ISBN: true,
            Autore: true,
            Editore: true,
            Edizione: true,
            Prezzo: true,
            Materia: true,
            Utente:{
                select:{
                    Nome: true,
                    Cognome: true,
                    Telefono: true,
                    Citta: true,
                    Email: true
                }
            },
            Immagine: true 
        }
    }).then(result => {
        const img = getImmagineLibro(result.Immagine);
        res.status(201).json({
            Libro: result,
            Stream: img
        });
     })
     .catch(err => {
         res.status(500).json({
             error: err
         });
     });
});


router.get("/cercaPerUtente/:utenteId", async (req, res) => {
    req.data = await db.libro.findMany({
        where: {
            Utente_id: parseInt(req.params.utenteId)
        },
        select: {
            Id: true,
            Nome: true,
            ISBN: true,
            Autore: true,
            Editore: true,
            Edizione: true,
            Prezzo: true,
            Materia: true,
            Immagine: true
        }
    }).then(result => {
         res.status(200).json(result);
     })
     .catch(err => {
         res.status(500).json({
             error: err
         });
     });
});

router.delete("/:libroId", async (req, res, next) => {
    const deleteLibro = await db.libro.delete({
        where: {
            Id: parseInt(req.params.libroId)
        }
    }).catch((err) => {
        return res.status(500).json({
            error: err
        })
    });

    res.status(200).json({
        message: 'libro eliminato',
        Id: req.params.Id
    });
});





module.exports = router;