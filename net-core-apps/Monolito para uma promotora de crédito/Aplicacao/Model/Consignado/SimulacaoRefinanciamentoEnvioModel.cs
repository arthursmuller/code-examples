using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Consignado
{
    public class SimulacaoRefinanciamentoEnvioModel
    {
        [Required(ErrorMessage = "Convênio é obrigatório.")]
        public int IdConvenio { get; set; }

        public decimal? ValorOperacao { get; set; }

        public decimal? Prestacao { get; set; }

        public string Plano { get; set; }

        public string Prazo { get; set; }

        public int[] Prazos { get; set; }

        public bool RetornarSomenteOperacoesViaveis { get; set; }

        public long? Proposta { get; set; }

        public IEnumerable<SimulacaoRefinanciamentoContratoEnvioModel> ContratosRefinanciamento { get; set; }
    }
}