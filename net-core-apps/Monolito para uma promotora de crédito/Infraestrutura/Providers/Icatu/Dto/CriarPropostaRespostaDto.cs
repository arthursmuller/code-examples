using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]

    public class CriarPropostaRespostaDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Mensagem { get; set; }
    }
}
