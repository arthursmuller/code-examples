using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Consignado
{
    [ExcludeFromCodeCoverage]
    public class SimulacaoPortabilidadeModel
    {
        public ViabilidadePortabilidadeModel Viabilidade { get; set; }

        public IEnumerable<SimulacaoRefinanciamentoModel> SimulacoesIntencaoRefinanciamento { get; set; }
    }
}
