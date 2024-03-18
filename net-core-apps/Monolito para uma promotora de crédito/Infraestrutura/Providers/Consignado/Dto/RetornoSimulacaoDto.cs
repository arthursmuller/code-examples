using System;

namespace Infraestrutura.Providers.Consignado.Dto
{
    public class RetornoSimulacaoDto
    {
        public string Plano { get; set; }

        public string TipoTabela { get; set; }

        public string DescricaoPlano { get; set; }

        private string _prazo;

        public string Prazo { get => _prazo; set => _prazo = value?.PadLeft(3, '0'); }

        public DateTime Emissao { get; set; }

        public DateTime PrimeiroVcto { get; set; }

        public decimal ValorOperacao { get; set; }

        public decimal Prestacao { get; set; }

        public decimal ValorIOF { get; set; }

        public decimal ValorFinanciado { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ValorAF { get; set; }

        public decimal? ValorPMR { get; set; }

        public decimal TaxaMes { get; set; }

        public decimal TaxaAno { get; set; }

        public decimal CetMes { get; set; }

        public decimal CetAno { get; set; }

        public decimal TirDiferencial { get; set; }

        public bool OperacaoViavel { get; set; }

        public string CodCritica { get; set; }

        public string MsgCritica { get; set; }
    }
}
