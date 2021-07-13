require('dotenv').config();
const S3 = require('aws-sdk/clients/s3');
const fs = require('fs');


const bucketName = process.env.S3_BUCKET;
const region = process.env.S3_REGION;
const accessKeyId = process.env.S3_ACCESS_KEY;
const secretAccessKey = process.env.S3_SECRET_KEY;


const s3 = new S3({
    region,
    accessKeyId,
    secretAccessKey
});

exports.s3 = s3;

function uploadFile (file) {
    const fileStream = fs.createReadStream(file.path);

    const uploadParams = {
        Bucket: bucketName,
        Body: fileStream,
        Key: file.filename,
        ContentType: "image/jpeg"
    };

    return s3.upload(uploadParams).promise();
};
exports.uploadFile = uploadFile;

function getFileStream(fileKey) {
    const downloadParams = {
        Key: fileKey,
        Bucket: bucketName,
        ResponseContentType: "image/jpeg"
    };
    return s3.getObject(downloadParams).createReadStream();
}
exports.getFileStream = getFileStream;