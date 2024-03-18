namespace Signature.Domain.Dtos
{
    public class VerifyPdfSignatureDto
    {
        public bool Verified { get; set; }
        public bool Authenticity { get; set; }
        public bool Integrity { get; set; }
        public bool Expired { get; set; }
        public VerifyPdfSignatureDto(bool verified, bool authenticity, bool integrity, bool expired)
        {
            Verified = verified;
            Authenticity = authenticity;
            Integrity = integrity;
            Expired = expired;
        }
    }
}
