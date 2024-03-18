using System;
using Signature.Domain.Abstractions;

namespace Signature.Domain.AggregatesModel.SignatureAggregate
{
    public class SignatureInformation : BaseEntity, IAggregateRoot
    {
        public int UserId { get; set; }
        public string UserIdentification { get; set; }
        public string UserName { get; set; }
        public string UserCellphone { get; set; }
        public string UserEmail { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductDatabaseRecordId { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentUrl { get; set; }
        public string UserPictureUrl { get; set; }
        public string SignatureDrawingUrl { get; set; }
        public string CertificateUrl { get; private set; }
        public string DocumentFileExtension { get; set; }
        public DateTime? CertificateGenerationDate { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string UserIpAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public SignatureInformation() { }
        public SignatureInformation(
            int userId, 
            string userIdentification,
            string userName,
            string userCellphone,
            string userEmail,
            int productTypeId,
            int productDatabaseRecordId,
            string documentNumber,
            string documentUrl, 
            string userPictureUrl, 
            string signatureDrawingUrl,
            string certificateUrl,
            string documentFileExtension,
            string longitude, 
            string latitude, 
            string userIpAddress, 
            DateTime? certificateGenerationDate, 
            string city, 
            string state, 
            string country, 
            string postalCode)
        {
            UserId = userId;
            UserIdentification = userIdentification;
            UserName = userName;
            UserCellphone = userCellphone;
            UserEmail = userEmail;
            ProductTypeId = productTypeId;
            ProductDatabaseRecordId = productDatabaseRecordId;
            DocumentNumber = documentNumber;
            DocumentUrl = documentUrl;
            DocumentFileExtension = documentFileExtension;
            Longitude = longitude;
            Latitude = latitude;
            UserIpAddress = userIpAddress;
            CertificateGenerationDate = certificateGenerationDate;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            UserPictureUrl = userPictureUrl;
            SignatureDrawingUrl = signatureDrawingUrl;
            CertificateUrl = certificateUrl;
        }


        public SignatureInformation(
           int userId,
           int productTypeId,
           int productDatabaseRecordId,
           string documentNumber,
           string documentUrl,
           string userPictureUrl,
           string certificateUrl,
           string longitude,
           string latitude,
           string userIpAddress,
           DateTime? certificateGenerationDate,
           string city,
           string state,
           string country,
           string postalCode)
        {
            UserId = userId;
            ProductTypeId = productTypeId;
            ProductDatabaseRecordId = productDatabaseRecordId;
            DocumentNumber = documentNumber;
            DocumentUrl = documentUrl;
            Longitude = longitude;
            Latitude = latitude;
            UserIpAddress = userIpAddress;
            CertificateGenerationDate = certificateGenerationDate;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            UserPictureUrl = userPictureUrl;
            CertificateUrl = certificateUrl;
        }

        public void AddCertificate(string certificateUrl) => CertificateUrl = certificateUrl;
        public void OverrideCreatedDate(DateTime date) => _createdDate = date;
        public void AddDocumentUrl(string documentUrl) => DocumentUrl = documentUrl;
    }
}
