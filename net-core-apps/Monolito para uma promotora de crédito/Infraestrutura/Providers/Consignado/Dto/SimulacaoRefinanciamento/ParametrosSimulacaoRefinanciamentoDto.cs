using System;
using System.Collections.Generic;

namespace Infraestrutura.Providers.Consignado.Dto
{
    public class ParametrosSimulacaoRefinanciamentoDto
    {
        public string CPF { get; set; }

        public string Conveniada { get; set; }

        public decimal? ValorOperacao { get; set; }

        public decimal? Prestacao { get; set; }

        public string Plano { get; set; }

        public string Prazo { get; set; }

        public int[] Prazos { get; set; }

        public DateTime? DataNascimento { get; set; }

        public bool RetornarSomenteOperacoesViaveis { get; set; }

        public long? Proposta { get; set; }

        public IEnumerable<ContratoSimulacaoRefinDto> ContratosRefinanciamento { get; set; }
    }
}
