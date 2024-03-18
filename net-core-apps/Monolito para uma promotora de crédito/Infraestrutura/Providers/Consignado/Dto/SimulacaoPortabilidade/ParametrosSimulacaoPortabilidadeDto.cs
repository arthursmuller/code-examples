using System;

namespace Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade
{
    public class ParametrosSimulacaoPortabilidadeDto
    {
        public string IFOriginadora { get; set; }

        public string Conveniada { get; set; }

        public string PrazoRestante { get; set; }

        public string PrazoTotal { get; set; }

        public decimal? SaldoDevedor { get; set; }

        public decimal? ValorPrestacaoPortabilidade { get; set; }

        public decimal? ValorPrestacaoDesejada { get; set; }

        public int[] Prazos { get; set; }

        public DateTime? DataNascimento { get; set; }

        public bool RetornarSomenteOperacoesViaveis { get; set; }

        public string PlanoRefin { get; set; }

        public string PrazoRefin { get; set; }

        public decimal? ValorPrestacaoRefin { get; set; }

        public bool? SimulacaoEspecial { get; set; }
    }
}
