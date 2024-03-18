using Signature.Domain.AggregatesModel.SignatureAggregate;

namespace Signature.Domain.Dtos
{
    public class GenerateSignatureDto
    {
        public bool Signed { get; set; }
        public SignatureInformation Signature { get; set; }

        public GenerateSignatureDto(bool signed, SignatureInformation signature)
        {
            Signed = signed;
            Signature = signature;
        }
    }
}
