const jwt = require('jsonwebtoken');

/*Use this middleware to protect methods because require an header field 'Authorization' in which
  we set the token of the user and the jwt.verify method verifies if it's valid */
module.exports = (req, res, next) => {
    try {
        const token = req.headers.authorization.split(" ")[1];
        console.log(token);
        const decode = jwt.verify(token, process.env.JWT_KEY);
        req.userData = decoded;
    } catch (error) {
        return res.status(401).json({
            message: 'Auth failed'
        });
    }
    next();
} 