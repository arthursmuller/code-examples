namespace Signature.Domain.Dtos
{
    public class GetSignatureDto
    {
        public string SignatureDrawingUrl { get; set; }
        public string SignatureDate { get; set; }
        public GetSignatureDto(string signatureDrawingUrl, string signatureDate)
        {
            SignatureDrawingUrl = signatureDrawingUrl;
            SignatureDate = signatureDate;
        }
        public GetSignatureDto() { }
    }
}
