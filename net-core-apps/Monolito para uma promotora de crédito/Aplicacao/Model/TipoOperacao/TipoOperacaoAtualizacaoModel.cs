using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.TipoOperacao
{
    public class TipoOperacaoAtualizacaoModel
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sigla é obrigatório.")]
        public string Sigla { get; set; }
    }
}
