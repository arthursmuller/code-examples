using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]
    public class CriarNumeroPropostaDto
    {
        public int TipoNumeracaoProposta { get; set; }
        public int GrupoApolice { get; set; }
        public int Subestipulante { get; set; }
        public int Modulo { get; set; }
    }
}
