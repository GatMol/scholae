require('dotenv').config()
const S3 = require('aws-sdk/clients/s3')
const fs = require('fs')

const bucketName = process.env.S3_BUCKET
const region = process.env.S3_REGION
const accessKeyId = process.env.S3_ACCESS_KEY
const secretKey = process.env.S3_SECRET_KEY


const s3 = new S3({
    region,
    accessKeyId,
    secretAccessKey
})

export function uploadFile(fileStream, fileName) {

    const uploadParams = {
        Bucket: bucketName,
        Body: fileStream,
        Key: fileName
    }

    return s3.upload(uploadParams).promise()
}
exports.uploadFile = uploadFile