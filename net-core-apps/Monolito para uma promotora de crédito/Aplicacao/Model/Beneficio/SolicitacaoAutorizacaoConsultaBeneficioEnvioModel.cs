using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Beneficio
{
    public class SolicitacaoAutorizacaoConsultaBeneficioEnvioModel
    {
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [Required]
        public int IdTelefoneEnvioSolicitacao { get; set; }
    }
}
