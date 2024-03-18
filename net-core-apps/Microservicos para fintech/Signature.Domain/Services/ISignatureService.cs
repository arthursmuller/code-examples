using System;
using System.Threading.Tasks;
using Signature.Domain.Dtos;

namespace Signature.Domain.Services
{
    public interface ISignatureService
    {
        Task<VerifyPdfSignatureDto> VerifyPdfFile(string base64);
        Task<GetSignatureDto> GetSignatureUrl(int signatureId);
        Task<bool> SignDocument(int userId, int productTypeId, int productRecordId, string documentUrl, string userPictureUrl, string longitude, string latitude, string userIpAddress);
        Task<GenerateSignatureDto> GenerateSignature(
            int documentId,
            int userId,
            int productTypeId,
            int productRecordId,
            DateTime documentCreatedDate,
            string documentUrl,
            byte[] document,
            string documentExtension,
            string signerIdentification,
            string signerName,
            string signerCellphone,
            string signerEmail,
            string documentNumber,
            string userIpAddress,
            string longitude,
            string latitude,
            string signaturePictureBase64string,
            string base64SignatureString,
            bool? saveSignatureOnly = false);

        Task UpdateSignatureDrawing(int signatureId, string base64);
        Task UpdateSignaturePicture(int signatureId, string base64);
        Task UpdateCreatedDate(int signatureId, string newDate);
        Task<GenerateSignatureDto> Override(
            int signatureId,
            byte[] document,
            string signerName,
            string documentNumber);
        Task Delete(int signatureId);
        Task<DownloadDto> DownloadCertificate(string documentNumber);
    }
}
