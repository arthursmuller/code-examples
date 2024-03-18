using System;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Consignado
{
    [ExcludeFromCodeCoverage]
    public class ViabilidadePortabilidadeModel
    {
        public bool Portavel { get; set; }

        public string Mensagem { get; set; }

        public decimal ParcelaMinima { get; set; }

        public decimal Tir { get; set; }

        public decimal ValorRCOBruto { get; set; }

        public decimal ValorRCOLiquido { get; set; }

        public decimal SaldoDevedorCorrigido { get; set; }

        public DateTime PrimeiroVcto { get; set; }

        public DateTime Emissao { get; set; }

        public decimal ValorOperacao { get; set; }

        public decimal Prestacao { get; set; }

        public decimal ValorIOF { get; set; }

        public decimal ValorFinanciado { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal TaxaMes { get; set; }

        public decimal TaxaAno { get; set; }

        public decimal CetMes { get; set; }

        public decimal CetAno { get; set; }
    }
}
