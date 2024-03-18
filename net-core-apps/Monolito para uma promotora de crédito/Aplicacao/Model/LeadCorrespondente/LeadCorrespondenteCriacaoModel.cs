using System.ComponentModel.DataAnnotations;
using Aplicacao.CustomValidation;

namespace Aplicacao
{
    public class LeadCorrespondenteCriacaoModel
    {
        [Required]
        [CNPJ]
        public string CNPJ { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int IdMunicipio { get; set; }

        public string Atividades { get; set; }
    }
}