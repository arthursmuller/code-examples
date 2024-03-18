using Dominio.Enum;
using System;

namespace Dominio
{
    public class SeguroProdutoDominio : EntidadeBase
    {
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public decimal ValorPremio { get; set; }
        public Produto IdProduto { get; private set; }
        public ProdutoDominio Produto { get; set; }

        public SeguroProdutoDominio(string descricao, string nome, DateTime dataInicioVigencia, DateTime dataFimVigencia, Produto idProduto, decimal valorPremio)
        {
            Descricao = descricao;
            Nome = nome;
            DataInicioVigencia = dataInicioVigencia;
            DataFimVigencia = dataFimVigencia;
            IdProduto = idProduto;
            ValorPremio = valorPremio;
        }
    }
}
