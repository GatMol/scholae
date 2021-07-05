const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

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
   await prisma.libro.findMany().then(result => {
        res.status(200).json(result);
    })
    .catch(err => {
        res.status(500).json({
            error: err
        });
    });
});

router.get("/nome", async (req, res, next) => {
    const libri = await prisma.libro.findUnique()({
        where: {
            Nome: req.params.nome
        }
    }).catch((err) => {
        return res.status(500).json({
            error: err
        });
    });

    return res.json({
        Id: libri.Id
    });
});

router.get("/materia", async (req, res, next) => {
    const libri = await prisma.libro.findUnique({
        where: {
            Materia: req.params.materia
        }
    }).catch((err) => {
        return res.status(500).json({
            error: err
        });
    });

    return res.json({
        Id: libri.Id
    });
});

router.get("/:libroId", (req, res) => {
    prisma.libro.findUnique({
        where: {
            Id: parseInt(req.params.libroId)
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