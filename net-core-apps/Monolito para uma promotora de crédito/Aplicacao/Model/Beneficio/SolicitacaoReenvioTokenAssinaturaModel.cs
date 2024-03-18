using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Beneficio
{
    public class SolicitacaoReenvioTokenAssinaturaModel
    {
        [Required]
        public int IdConsultaBeneficio { get; set; }

        [Required]
        public int IdTelefoneEnvioSolicitacao { get; set; }
    }
}
