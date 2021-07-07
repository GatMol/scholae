const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

router.get("/getLibriSalvati/:utenteId", async (req, res, next) => {
    await prisma.libroSalvato.findMany({
        where: {
            Utente: parseInt(req.params.utenteId)
        }
    })
    .then(result => {
        res.status(201).json(result);
    })
    .catch(err => {
        return res.status(500).json({
            error: err
        });
    });
});

router.post("/", async (req, res, next) =>{

    const libroS = {
        Libro_id: req.body.Libro_id,
        Utente_id: req.body.Utente_id
    }

    await prisma.libroSalvato.create({data: libroS})
    .then(result => {
        res.status(201).json(libroS);
    })
    .catch(err => {
        console.log(err);
        res.status(500).json({
            error: err
        });
        });
});

router.get("/get", async (req, res, next) =>{

     libroS = await prisma.libroSalvato.findMany({
        select: {
            Libro: true,
            Utente: true
        },
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


router.get("/libriSalvati/:utenteId", async(req, res, next) => {
    
    await prisma.libroSalvato.findMany({
        select: {
            Libro:true
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