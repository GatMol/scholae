const express = require("express");
const app = express();

const cors = require("cors");
const morgan = require("morgan");

const Pool = require("pg").Pool;
const pool = new Pool({
    user: "gatto",
    host: "scholae-database.cu9bh0ufrged.us-east-2.rds.amazonaws.com",
    database: "myDB",
    password: "scholaemobile2021",
    port: 5432
});

const utenteRoute = require('./routes/utente');
const libroRoute = require('./routes/libro');
const materiaRoute = require('./routes/materia');
const libroSalvatoRoute = require("./routes/libroSalvato");

app.use(cors());
app.use(morgan('dev'));
app.use(express.json());

/*Append headers to any response we send back to say to the client*/
app.use((req, res, next) => {
    /*this will not send the response, just adjust it. 
    Wherever I do send a response it has these headers*/
    res.header('Access-Control-Allow-Origin', '*');
    res.header(
        'Access-Control-Allow-Headers', 
        'Origin, X-Requested-With, Content-Type, Accept, Authorization'
    );
    /*browser will always send OPTIONS request before you send a POST request or PUT*/
    if(req.method === 'OPTIONS'){
        res.header('Access-Control-Allow-Methods', 'PUT, POST, PATCH, DELETE, GET');
        /*we don't want to go to my routes, because OPTIONS req is just for finding out what options we have,
        and we have such an answer provided by these CrossOriginResourceShared headers (CROS forced by browser)*/
        return res.status(200).json({});
    }
    /*block incoming request if not an OPTIONS req, so that other routes take over*/
    next();
});

app.use("/utente", utenteRoute);
app.use("/libro", libroRoute);
app.use("/materia", materiaRoute);
app.use("/libroSalvato", libroSalvatoRoute);

/*if request reaches this line, return error because we don't handle that path*/
app.use((req, res, next) => {
    const error = new Error("Not found");
    error.status = 404;
    next(error);
});

app.use((error, req, res, next) => {
    res.status(error.status || 500);
    res.json({
        error: {
            message: error.message
        }
    });
});

module.exports = app;