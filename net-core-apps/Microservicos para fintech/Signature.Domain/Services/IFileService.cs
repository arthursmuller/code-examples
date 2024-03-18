using System;

namespace Signature.Domain.Services
{
    public interface IFileService
    {
        byte[] GenerateDigitalSignature(
            string identification,
            string name,
            string cellphone,
            string email,
            string documentNumber,
            string originalDocumentHash,
            string originalDocumentSignedHash,
            DateTime signatureDate,
            DateTime signatureGenerationDate,
            string signatureIp,
            string signatureLatitude,
            string signatureLongitude,
            string signatureCity,
            string signatureState,
            string signatureCountry,
            string signaturePostalCode,
            string signatureLocationLatitude,
            string signatureLocationLongitude,
            string signaturePictureBase64string,
            string base64SignatureString);
    }
}
