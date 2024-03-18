using AddressProvider;
using BrDateTimeUtils;
using Signature.Domain.Services;
using System;
using System.Threading.Tasks;
using Signature.Domain.AggregatesModel.SignatureAggregate;
using Signature.Domain.Dtos;
using Signature.Infraestructure.Persistence;
using Signature.Infraestructure.Providers.Storage;
using FileHelper;
using SignPdfProvider.Abstractions;
using Microsoft.EntityFrameworkCore;
using Signature.Domain.Exceptions;
using System.Linq;
using System.Collections.Generic;

namespace Signature.Infraestructure.Services
{
    public class SignatureService : BaseService, ISignatureService
    {
        private readonly IFileService _fileService;
        private readonly IStorage _storageProvider;
        private readonly ISignPdfService _signPdfService;
        private readonly AddressService _addressService;
        public SignatureService(
            SignatureContext context,
            IIdentityService identityService,
            ISignatureRepository signatureRepository,
            IStorage storageProvider,
            IFileService fileService,
            ISignPdfService signPdfService,
            AddressService addressService) : base(context, signatureRepository, identityService)
        {
            _fileService = fileService;
            _addressService = addressService;
            _signPdfService = signPdfService;
            _storageProvider = storageProvider;
        }

        public async Task<bool> SignDocument(
            int userId,
            int productTypeId,
            int productRecordId,
            string documentUrl,
            string userPictureUrl,
            string longitude,
            string latitude,
            string userIpAddress)
        {
            var address = await _addressService.Search(null, userIpAddress);
            var newSignature = new SignatureInformation(
                userId,
                productTypeId,
                productRecordId,
                null,
                documentUrl,
                userPictureUrl,
                null,
                longitude,
                latitude,
                userIpAddress,
                DateTime.Now.Brasilia(),
                address.GeoBase?.City,
                address.GeoBase?.State,
                address.GeoBase?.Country_name,
                address.GeoBase?.Postal);

            await addAndSaveAsync(newSignature);

            return true;
        }

        public async Task<GenerateSignatureDto> GenerateSignature(
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
            bool? saveSignatureOnly = false)
        {
            string certificateUrl = null;
            var signaturePictureUrl = await _storageProvider.UploadUserPicture(signaturePictureBase64string, $"{documentCreatedDate:yyyy-mm-dd}-{documentNumber}{userId}-picture", userId);
            var signatureDrawingUrl = await _storageProvider.UploadUserSignature(base64SignatureString, $"{documentCreatedDate:yyyy-mm-dd}-{documentNumber}{userId}-draw", userId);

            var exists = await _signatureContext
                .Signatures
                .AsNoTracking()
                .AnyAsync(e => e.DocumentNumber == documentNumber);

            if (exists) return default;

            var signatureDate = DateTime.Now.Brasilia();
            var address = await _addressService.Search(null, userIpAddress);

            var (hash, signedHash) = await generateOriginalDocumentHash(document);
            var signatureCertificatePage = _fileService.GenerateDigitalSignature(
                signerIdentification,
                signerName,
                signerCellphone,
                signerEmail,
                documentNumber,
                hash,
                signedHash,
                signatureDate,
                documentCreatedDate,
                userIpAddress,
                latitude,
                longitude,
                address.GeoBase?.City,
                address.GeoBase?.State,
                address.GeoBase?.Country_name,
                address.GeoBase?.Postal,
                $"{address.GeoBase?.Latitude}",
                $"{address.GeoBase?.Longitude}",
                signaturePictureBase64string,
                base64SignatureString);

            if (!(saveSignatureOnly ?? false))
            {
                var signedCertificate = await signDocument(signatureCertificatePage);
                certificateUrl = await _storageProvider.UploadFinancingProductContractCertificates(signedCertificate, documentNumber, userId);
                var signedDocument = await signDocument(document, signatureCertificatePage, documentExtension);
                await _storageProvider.Update(documentUrl, signedDocument);
            }
            else
            {
                await _storageProvider.Update(documentUrl, Convert.ToBase64String(document));
            }

            var newSignature = new SignatureInformation(
                userId,
                signerIdentification,
                signerName,
                signerCellphone,
                signerEmail,
                productTypeId,
                productRecordId,
                documentNumber,
                documentUrl,
                signaturePictureUrl,
                signatureDrawingUrl,
                certificateUrl,
                documentExtension,
                longitude ?? $"{address.GeoBase?.Latitude}",
                longitude ?? $"{address.GeoBase?.Longitude}",
                userIpAddress,
                signatureDate,
                address.GeoBase?.City,
                address.GeoBase?.State,
                address.GeoBase?.Country_name,
                address.GeoBase?.Postal);

            await addAndSaveAsync(newSignature);

            return new GenerateSignatureDto(true, newSignature);
        }

        public async Task<GenerateSignatureDto> Override(
            int signatureId,
            byte[] document,
            string signerName,
            string documentNumber)
        {
            var signature = await _signatureContext
                .Signatures
                .FirstOrDefaultAsync(e => e.Id == signatureId);
            var (hash, signedHash) = await generateOriginalDocumentHash(document);
            var pictureFile = await _storageProvider.Get(signature.UserPictureUrl);
            var signatureFile = await _storageProvider.Get(signature.SignatureDrawingUrl);
            /*signature.OverrideCreatedDate(DateTime.Now.Brasilia());*/
            signature.UserName = signerName;

            var signatureCertificatePage = _fileService.GenerateDigitalSignature(
                signature.UserIdentification,
                signature.UserName,
                signature.UserCellphone,
                signature.UserEmail,
                documentNumber,
                hash,
                signedHash,
                signature.CreatedDate,
                signature.CreatedDate,
                signature.UserIpAddress,
                signature.Latitude,
                signature.Longitude,
                signature.City,
                signature.State,
                signature.Country,
                signature.PostalCode,
                $"{signature.Latitude}",
                $"{signature.Longitude}",
                Convert.ToBase64String(pictureFile.Blob),
                Convert.ToBase64String(signatureFile.Blob));

            var signedCertificate = await signDocument(signatureCertificatePage);
            await _storageProvider.Update(signature.CertificateUrl, signedCertificate);
            var signedDocument = await signDocument(document, signatureCertificatePage, signature.DocumentFileExtension);
            await _storageProvider.Update(signature.DocumentUrl, signedDocument);

            await saveChangesAsyncConcurrency();
            return new GenerateSignatureDto(true, signature);
        }

        public async Task UpdateSignatureDrawing(int signatureId, string base64)
        {
            var signature = await _signatureContext
                .Signatures
                .FirstOrDefaultAsync(e => e.Id == signatureId);

            await _storageProvider.Update(signature.SignatureDrawingUrl, base64);
        }
        
        public async Task UpdateSignaturePicture(int signatureId, string base64)
        {
            var signature = await _signatureContext
                .Signatures
                .FirstOrDefaultAsync(e => e.Id == signatureId);

            await _storageProvider.Update(signature.UserPictureUrl, base64);
        }

        public async Task UpdateCreatedDate(int signatureId, string newDate)
        {
            var signature = await _signatureContext
                .Signatures
                .FirstOrDefaultAsync(e => e.Id == signatureId);

            signature.OverrideCreatedDate(DateTime.ParseExact(newDate, "yyyy-MM-dd HH:mm:ss.fff",
                                       System.Globalization.CultureInfo.InvariantCulture));

            await saveChangesAsyncConcurrency();
        }

        public async Task<GetSignatureDto> GetSignatureUrl(int signatureId)
        {
            var signature = await _signatureContext
                .Signatures
                .FirstOrDefaultAsync(e => e.Id == signatureId);

            return new GetSignatureDto(signature.SignatureDrawingUrl, signature.CreatedDate.FromDate());
        }

        public async Task<VerifyPdfSignatureDto> VerifyPdfFile(string base64)
        {
            var res = await _signPdfService.VerifyFile(base64);
            return new VerifyPdfSignatureDto(res.Verified, res.Authenticity, res.Integrity, res.Expired);
        }

        public async Task<DownloadDto> DownloadCertificate(string documentNumber)
        {
            var signature = await _signatureContext
                .Signatures
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.DocumentNumber == documentNumber);

            if (signature is null)
                throw new SignatureException("Signature Not Found");

            var doc = await _storageProvider.Get(signature.CertificateUrl);

            if (doc is null)
                throw new SignatureException("Unable to obtain certificate from storage provider");

            return new DownloadDto(doc.Namespace, doc.FileName, doc.MimeType, doc.Extension, doc.Blob) ;
        }

        public async Task Delete(int signatureId)
        {
            var signature = await _signatureContext
                .Signatures
                .FirstOrDefaultAsync(e => e.Id == signatureId);

            if(signature is not null)
            {
                if(!string.IsNullOrEmpty(signature.DocumentUrl))
                    await _storageProvider.Delete(signature.DocumentUrl);
                if(!string.IsNullOrEmpty(signature.CertificateUrl))
                    await _storageProvider.Delete(signature.CertificateUrl);

                _signatureContext.Remove(signature);

                await saveChangesAsyncConcurrency();
            }
        }

        private async Task<(string, string)> generateOriginalDocumentHash(byte[] doc)
        {
            var result = HashSigner.GenerateSignatureHash(doc);
            return await Task.FromResult((result.Original, result.Signed));
        }

        private async Task<string> signDocument(byte[] document)
        {
            return await _signPdfService.SignFile(Convert.ToBase64String(document));
        }

        private async Task<string> signDocument(byte[] document, byte[] certificate, string extension)
        {
            if(!string.IsNullOrEmpty(extension))
                return await signZipFiles(document, certificate, extension);
            else
                return await signDocument(FileHelperService.JoinDocuments(document, certificate));
        }

        private async Task<string> signZipFiles(byte[] document, byte[] certificate, string extension)
        {
            if (extension.ToLower() == "zip")
            {
                var files = FileHelperService.GetFilesFromZip(document);
                var signedFilesBase64 = await _signPdfService
                        .SignFiles(
                            files.ToList().Select(e =>
                                Convert.ToBase64String(FileHelperService.JoinDocuments(e.Value, certificate)))
                            .ToArray());

                var signedFilesToCompress = new Dictionary<string, byte[]>();

                for (int i = 0; i < signedFilesBase64.Length; i++)
                    signedFilesToCompress.Add(
                        files.ElementAt(i).Key, Convert.FromBase64String(signedFilesBase64[i]));

                var signedFilesZipped = FileHelperService.CompressFiles(signedFilesToCompress);
                return Convert.ToBase64String(signedFilesZipped);
            }

            return default;
        }

    }
}
