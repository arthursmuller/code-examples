using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Produto
{
    public class ProdutoAtualizacaoModel
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sigla é obrigatório.")]
        public string Sigla { get; set; }

        public bool RequerConvenio { get; set; }
    }
}
