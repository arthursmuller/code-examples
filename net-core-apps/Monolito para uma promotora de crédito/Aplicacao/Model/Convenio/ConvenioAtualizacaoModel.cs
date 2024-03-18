using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Convenio
{
    public class ConvenioAtualizacaoModel
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Código é obrigatório.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Convênio inválido. Verifique a quantidade de dígitos.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Convênio inválido. Verifique se o mesmo é composto somente por números.")]
        public string Codigo { get; set; }

        public string Descricao { get; set; }
    }
}
