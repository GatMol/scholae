const express = require('express');
const prisma = require('../services/database');
const router = express.Router();
const db = require("../services/database");

router.get("/", async (req, res, next) =>{
    prisma.materia.findMany({
        orderBy: {
            Nome: "asc"
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

module.exports = router;