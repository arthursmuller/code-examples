using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.AcessoDadosPessoais
{
    [ExcludeFromCodeCoverage]
    public class SolicitacaoAcessoDadosPessoaisTelefoneEnvioModel
    {
        [Required]
        public string DDD { get; set; }
        [Required]
        public string Telefone { get; set; }
    }
}
