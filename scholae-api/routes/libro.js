const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

BigInt.prototype.toJSON = function() {       
    return this.toString()
  }
  

  router.post("/", upload.single('libroImage'), async (req, res, next)=> {
    //TODO: nella richiesta aggiungere un campo libroimage con il file (stream)
    const file = req.file;
    console.log(file);
    const result = await uploadFile(file);
    console.log(result);
    const newLibro = {
        ISBN: req.body.ISBN,
        Nome: req.body.Nome,
        Autore: req.body.Autore,
        Edizione: req.body.Edizione,
        Editore: req.body.Editore,
        Prezzo: req.body.Prezzo,
        Materia_id: req.body.Materia_id,
        Utente_id: parseInt(req.body.Utente_id),
        Immagine: file.filename
    }

    await prisma.libro.create({data: newLibro})
    .then(result => {
        console.log(result);
        res.status(201);
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

router.get("/cercaPerId/:libroId", async (req, res) => {
    req.data = await db.libro.findUnique({
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


router.get("/cercaPerUtente/:utenteId", async (req, res) => {
    req.data = await db.libro.findMany({
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
            Utente: {
                    Id: parseInt(req.params.utenteId)
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

router.delete("/elimina/:libroId", async (req, res, next) => {
    const deleteLibro = await prisma.libro.delete({
        where: {
            Id: parseInt(req.params.libroId)
        }
    }).catch((err) => {
        return res.status(500).json({
            error: err
        })
    });
    res.status(200).json();
});

module.exports = router;