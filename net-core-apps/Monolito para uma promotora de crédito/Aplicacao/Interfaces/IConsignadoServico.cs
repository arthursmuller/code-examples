using Aplicacao.Model.Consignado;
using Aplicacao.Model.RendimentoCliente;
using Infraestrutura.Providers.Consignado.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IConsignadoServico
    {
        Task<IEnumerable<RetornoSimulacaoDto>> SimularNovo(SimulacaoNovoEnvioModel parametros);

        Task<IEnumerable<ContratoClienteModel>> ListarContratosCliente(ContratoClienteEnvioModel parametros);

        Task<IEnumerable<RetornoSimulacaoDto>> SimularRefinanciamento(SimulacaoRefinanciamentoEnvioModel parametros);

        Task<RendimentoSiapeConsultaMargemModel> ConsultarMargemSiape(string orgao, string matricula);

        Task<SimulacaoPortabilidadeModel> SimularPortabilidade(SimulacaoPortabilidadeEnvioModel parametrosSimulacao);
    }
}
