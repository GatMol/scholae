const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

BigInt.prototype.toJSON = function() {       
    return this.toString()
  }
  

router.post("/", async (req, res, next)=> {
    const newLibro = {
        ISBN: req.body.ISBN,
        Nome: req.body.Nome,
        Autore: req.body.Autore,
        Edizione: req.body.Edizione,
        Editore: req.body.Editore,
        Prezzo: req.body.Prezzo,
        Materia: req.body.Materia,
        Utente: req.body.Utente
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