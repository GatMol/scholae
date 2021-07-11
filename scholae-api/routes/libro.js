const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");
const multer  = require('multer');
const upload = multer({ dest: 'uploads/'});
const fs = require('fs');
const util = require('util');
const unlinkFile = util.promisify(fs.unlink);
const { uploadFile, getFileStream } = require('../s3');

BigInt.prototype.toJSON = function() {       
    return this.toString()
}

router.post('/images', upload.single('libroImage'), async (req, res) => {
    const file = req.file;
    console.log(file);
    const result = await uploadFile(file);
    console.log(result);
    return res.status(200).json({
        message: 'immagine salvata',
        file: file,
        s3: result
    })
});

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
    //TODO: nella richiesta aggiungere un campo libroimage con il file (stream)
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
            Immagine: file.filename
          }
    })
    .then(result => {
        console.log(result);
        res.status(201).json(result);
    })
    .catch(err => {
        console.log(err);
        res.status(500).json({
            error: err
        });
    });
});

router.get("/", async (req, res, next) => {
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
            } 
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
        ]
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
    return readStream;
    //TODO: Riporta in un formato la foto oppure come stream
    //TODO: Aggiungi un campo path nell immagine, associato alla key generata da multer e che lo identifica all'interno del bucket
    //cosi quando carichiamo il libro, carichiamo l'immagine facendo una get dell'immagine
    //TODO: Aggiungi nel POST del libro il campo path
};


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
    req.data = await db.libro.findUnique({
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