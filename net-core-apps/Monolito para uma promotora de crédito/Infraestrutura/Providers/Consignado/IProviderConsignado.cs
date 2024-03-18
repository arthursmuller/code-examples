using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Consignado
{
    public interface IProviderConsignado
    {
        Task<IEnumerable<RetornoSimulacaoDto>> SimularNovo(ParametrosSimulacaoNovoDto parametros, string tokenAutenticacao);

        Task<IEnumerable<RetornoContratoClienteDto>> ListarContratosCliente(ParametrosContratoClienteDto parametros, string tokenAutenticacao);

        Task<IEnumerable<RetornoSimulacaoDto>> SimularRefinanciamento(ParametrosSimulacaoRefinanciamentoDto parametros, string tokenAutenticacao);

        Task<RetornoConsultaMargemDto> ConsultarMargemSiape(ParametrosConsultaMargemDto parametros, string tokenAutenticacao);

        Task<byte[]> ObterTermoAutorizacaoBeneficiario(ParametrosAutorizacaoBeneficiarioDto parametros, string tokenAutenticacao);

        Task<RetornoSimulacaoPortabilidadeDto> SimularPropostaPortabilidade(ParametrosSimulacaoPortabilidadeDto parametros, string tokenAutenticacao);
    }
}
