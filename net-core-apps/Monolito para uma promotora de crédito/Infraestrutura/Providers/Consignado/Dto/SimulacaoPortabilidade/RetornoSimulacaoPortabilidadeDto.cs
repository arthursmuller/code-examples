using System.Collections.Generic;

namespace Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade
{
    public class RetornoSimulacaoPortabilidadeDto
    {
        public ViabilidadePortabilidadeDto Viabilidade { get; set; }

        public IEnumerable<RetornoSimulacaoDto> Simulacao { get; set; }
    }
}
