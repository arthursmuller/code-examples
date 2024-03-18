using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Beneficio
{
    public class ValidacaoTokenAssinaturaEnvioModel
    {
        [Required]
        public int IdConsultaBeneficio { get; set; }

        [Required]
        public string TokenConsulta { get; set; }
    }
}
