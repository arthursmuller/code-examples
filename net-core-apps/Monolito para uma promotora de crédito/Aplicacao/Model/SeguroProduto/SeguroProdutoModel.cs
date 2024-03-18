using Aplicacao.Model.Produto;
using Aplicacao.Model.SeguroCobertura;
using System;

namespace Aplicacao.Model.SeguroProduto
{
    public class SeguroProdutoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public decimal ValorPremio { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }

        public ProdutoModel Produto { get; set; }
    }
}
