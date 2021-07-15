const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

router.post("/", async (req, res, next) =>{

    const libroS = {
        Libro_id: req.body.Libro_id,
        Utente_id: req.body.Utente_id
    }

    await prisma.libroSalvato.create({data: libroS})
    .then(result => {
        res.status(201).json(result);
    })
    .catch(err => {
        console.log(err);
        res.status(500).json({
            error: err
        });
        });
});

router.get("/ottienimi", async (req, res, next) =>{
    console.log(req);
    await prisma.libroSalvato.findUnique({
        select: {
            Libro: {
                select: {
                    Id: true,
                    Nome: true,
                    ISBN: true,
                    Autore: true,
                    Editore: true,
                    Edizione: true,
                    Prezzo: true,
                    Utente: {
                        select: {
                            Nome: true, 
                            Cognome: true
                },
                Immagine: true
            }
                }
            }
        },
        where: {
            Libro_id_Utente_id: {
                Libro_id: parseInt(req.body.Libro_id),
                Utente_id: parseInt(req.body.Utente_id)
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


router.get("/cercaPerUtente/:utenteId", async(req, res, next) => {
    
    await prisma.libroSalvato.findMany({
        select: {
            Libro: {
                select: {
                    Id: true,
                    Nome: true,
                    ISBN: true,
                    Autore: true,
                    Editore: true,
                    Edizione: true,
                    Prezzo: true,
                    Utente: {
                        select: {
                            Nome: true, 
                            Cognome: true
                }
            },
            Immagine: true
                }
            }
        },
        where: {
            Utente_id: parseInt(req.params.utenteId)
        }
    }).then(result => {
        res.status(200).json(result);
    })
    .catch(err => {
        res.status(500).json({
            error: err
        });
    });
})

router.delete("/", async (req, res, next) =>{

    libroS = await prisma.libroSalvato.deleteMany({
       where: {
           Libro_id: req.body.Libro_id,
           Utente_id: req.body.Utente_id
       }
   })
   .catch(err => {
       console.log(err);
       res.status(500).json({
           error: err
       });
       });
       return res.status(200).json(libroS);
});

module.exports = router;