using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Comunicacao.Models;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Infraestrutura.Providers;
using Infraestrutura.Providers.Consignado;
using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade;
using Infraestrutura.Providers.Dto;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Consignado
{
    [ExcludeFromCodeCoverage]
    public class ProviderConsignado : ProviderBase, IProviderConsignado
    {
        private const string NOME_API_CONSIGNADO = "ProdutoConsignado";

        public ProviderConsignado(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderConsignado> logger)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) { }

        public async Task<IEnumerable<RetornoSimulacaoDto>> SimularNovo(ParametrosSimulacaoNovoDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                            .AddNomeApi(NOME_API_CONSIGNADO)
                            .AddUrlApi(_configuracaoProviders.ConsignadoApi)
                            .AddUrlMetodo("Consignado/Simulacao/SimularNovo")
                            .AddBody(parametros)
                            .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorSimulacao, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorSimulacao, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<IEnumerable<RetornoSimulacaoDto>>(_mensagens);
        }

        public async Task<IEnumerable<RetornoContratoClienteDto>> ListarContratosCliente(ParametrosContratoClienteDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                            .AddNomeApi(NOME_API_CONSIGNADO)
                            .AddUrlApi(_configuracaoProviders.ConsignadoApi)
                            .AddUrlMetodo("Consignado/Contrato/ListarContratosCliente")
                            .AddParametro("CpfCliente", parametros.CpfCliente, TipoParametro.QueryString)
                            .AddParametro("Matricula", parametros.Matricula, TipoParametro.QueryString)
                            .AddParametro("Proposta", parametros.Proposta, TipoParametro.QueryString)
                            .AddParametro("ApenasConveniadasAtivas", parametros.ApenasConveniadasAtivas, TipoParametro.QueryString)
                            .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorContratos, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorContratos, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<IEnumerable<RetornoContratoClienteDto>>(_mensagens);
        }

        public async Task<IEnumerable<RetornoSimulacaoDto>> SimularRefinanciamento(ParametrosSimulacaoRefinanciamentoDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                            .AddNomeApi(NOME_API_CONSIGNADO)
                            .AddUrlApi(_configuracaoProviders.ConsignadoApi)
                            .AddUrlMetodo("Consignado/Simulacao/SimularPropostaRefinanciamento")
                            .AddBody(parametros)
                            .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorSimulacaoRefinanciamento, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorSimulacaoRefinanciamento, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<IEnumerable<RetornoSimulacaoDto>>(_mensagens);
        }

        public async Task<RetornoConsultaMargemDto> ConsultarMargemSiape(ParametrosConsultaMargemDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                            .AddNomeApi(NOME_API_CONSIGNADO)
                            .AddUrlApi(_configuracaoProviders.ConsignadoApi)
                            .AddUrlMetodo("Consignado/Conveniada/Siape/ObterMargem")
                            .AddParametro("Cpf", parametros.Cpf, TipoParametro.QueryString)
                            .AddParametro("Matricula", parametros.Matricula, TipoParametro.QueryString)
                            .AddParametro("Orgao", parametros.Orgao, TipoParametro.QueryString)
                            .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorConsultaMargemSiape, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorConsultaMargemSiape, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<RetornoConsultaMargemDto>(_mensagens);
        }

        public async Task<byte[]> ObterTermoAutorizacaoBeneficiario(ParametrosAutorizacaoBeneficiarioDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                            .AddNomeApi(NOME_API_CONSIGNADO)
                            .AddUrlApi(_configuracaoProviders.ConsignadoApi)
                            .AddUrlMetodo("/Consignado/Proposta/Documento/ObterTermoAutorizacaoBeneficiario")
                            .AddParametro("nomeCliente", parametros.NomeCliente, TipoParametro.QueryString)
                            .AddParametro("cpf", parametros.Cpf.ToString(), TipoParametro.QueryString)
                            .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorAutorizacaoBeneficiario, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorAutorizacaoBeneficiario, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<byte[]>(_mensagens);
        }

        public async Task<RetornoSimulacaoPortabilidadeDto> SimularPropostaPortabilidade(ParametrosSimulacaoPortabilidadeDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                            .AddNomeApi(NOME_API_CONSIGNADO)
                            .AddUrlApi(_configuracaoProviders.ConsignadoApi)
                            .AddUrlMetodo("/Consignado/Simulacao/V2/SimularPropostaPortabilidade")
                            .AddBody(parametros)
                            .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorSimulacaoDePortabilidade, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderConsignado_NaoHouveSucessoNoRetornoDoProvedorSimulacaoDePortabilidade, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<RetornoSimulacaoPortabilidadeDto>(_mensagens);
        }
    }
}
