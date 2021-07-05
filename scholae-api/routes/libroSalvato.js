const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

router.get("/:utenteId", async (req, res, next) => {
    await prisma.libroSalvato.findMany({
        where: {
            Utente: parseInt(req.params.utenteId)
        }
    })
    .then(result => {
        console.log(result);
        res.status(201);
    })
    .catch(err => {
        return res.status(500).json({
            error: err
        });
    });
});

router.post("/", async (req, res, next) =>{

    const libroS = {
        Libro: req.body.Libro,
        Utente: req.body.Utente
    }

    await prisma.libroSalvato.create({data: libroS})
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

module.exports = router;