using Dominio.Enum;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao
{
    [ExcludeFromCodeCoverage]
    public class AnexoSolicitacaoModel
    {
        public string Motivo { get; set; }
        public string Solicitante { get; set; }
        public TipoDocumento IdTipoDocumento { get; set; }
        public int IdCliente { get; set; }
    }
}
