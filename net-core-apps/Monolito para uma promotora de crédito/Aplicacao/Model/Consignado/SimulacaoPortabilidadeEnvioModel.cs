using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Consignado
{
    [ExcludeFromCodeCoverage]
    public class SimulacaoPortabilidadeEnvioModel
    {
        [Required]
        public int IdRendimentoCliente { get; set; }

        public int PrazoRestante { get; set; }

        public int PrazoTotal { get; set; }

        public decimal? SaldoDevedor { get; set; }

        public decimal? ValorPrestacaoPortabilidade { get; set; }

        public decimal? ValorPrestacaoDesejada { get; set; }

        public int[] Prazos { get; set; }

        public bool RetornarSomenteOperacoesViaveis { get; set; }

        public string PlanoRefin { get; set; }

        public int? PrazoRefin { get; set; }

        public decimal? ValorPrestacaoRefin { get; set; }
    }
}
