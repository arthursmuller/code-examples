using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.IntencaoOperacao
{
    [ExcludeFromCodeCoverage]
    public class IntencaoOperacaoCriacaoModel
    {
        public int IdTipoOperacao { get; set; }
        public int IdProduto { get; set; }
        public int? IdRendimentoCliente { get; set; }
        public decimal Prestacao { get; set; }
        public decimal ValorAuxilioFinanceiro { get; set; }
        public decimal TaxaMes { get; set; }
        public decimal TaxaAno { get; set; }
        public decimal ValorFinanciado { get; set; }
        public int Prazo { get; set; }
        public decimal ImpostoOperacaoFinanceira { get; set; }
        public decimal CustoEfetivoTotalMes { get; set; }
        public decimal CustoEfetivoTotalAno { get; set; }
        public DateTime PrimeiroVencimento { get; set; }

        public IntencaoOperacaoPortabilidadeModel Portabilidade { get; set; }

        public IEnumerable<IntencaoOperacaoContratoModel> Contratos { get; set; }
    }
}
