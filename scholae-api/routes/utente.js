const express = require("express");
const router = express.Router();
const db = require("../services/database");
const bcrypt = require("bcrypt");
const jwt = require("jsonwebtoken");

router.post("/signup", async (req, res, next) => {
    const user = await db.utente.findUnique({ where: { Email: req.body.email } })
        .catch((err) => {
            console.log(err);
            return res.status(500).json({
                error: err
            });
        });

    if (user !== null) {
        return res.status(409).json({
            message: 'Email already taken',
        });
    } else {
        bcrypt.hash(req.body.password, 10, (err, hash) => {
            if (err) {
                return res.status(500).json({
                    error: err
                });
            }
            else {
                const newUtente = {
                    Email: req.body.email,
                    Nome: req.body.nome,
                    Cognome: req.body.cognome,
                    Password: hash,
                    Telefono: req.body.telefono,
                    Nazionalita: req.body.nazionalita
                }

                db.utente.create({ data: newUtente })
                    .catch((err) => {
                        return res.status(500).json({
                            error: err
                        });
                    });

                res.status(201).json({
                    message: 'User created',
                    Utente: newUtente
                });
            }
        });
    }
});

router.post("/login", async (req, res, next) => {
    const user = await db.utente.findFirst({ where: { Email: req.body.email } })
        .catch((err) => {
            console.log(err);
            return res.status(500).json({
                error: err
            });
        });

    if (user === null) {
        console.log(err);
        return res.status(401).json({
            message: 'Auth failed'
        });
    } else {
        bcrypt.compare(req.body.password, user.Password, (err, result) => {
            if (err) {
                console.log(err);
                return res.status(401).json({
                    message: 'Auth failed'
                });
            }
            if (result) {
                const token = jwt.sign(
                    {
                        email: user.Email,
                    },
                    process.env.JWT_KEY,
                    {
                        expiresIn: 60 * 30
                    }
                );
                return res.status(200).json({
                    message: 'Auth succeded',
                    token: token
                });
            }
            res.status(401).json({
                message: 'Auth failed'
            });
        });
    }
});

module.exports = router;