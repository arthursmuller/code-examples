using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Comunicacao.Models;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Infraestrutura.Consulta;
using Infraestrutura.Providers.Cliente.Dto;
using Infraestrutura.Providers.Cliente.Dto.ListaBeneficio;
using Infraestrutura.Providers.Cliente.Dto.NovaAutorizacao;
using Infraestrutura.Providers.Dto;
using Microsoft.Extensions.Logging;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Cliente
{
    [ExcludeFromCodeCoverage]
    public class ProviderCliente : ProviderBase, IProviderCliente
    {
        private const string NOME_API_CLIENTE = "Cliente";

        private readonly BeneficioInssMensagemDeParaQuery _beneficioInssMensagem;

        public ProviderCliente(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderCliente> logger, BeneficioInssMensagemDeParaQuery beneficioInssMensagem)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger)
            => _beneficioInssMensagem = beneficioInssMensagem;

        public async Task<ClienteDto> ObterDadosCliente(string cpf, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                                    .AddNomeApi(NOME_API_CLIENTE)
                                    .AddUrlApi(_configuracaoProviders.ClienteApi)
                                    .AddUrlMetodo("Pessoa/ObterDadosCliente/{cpf}")
                                    .AddParametro("cpf", cpf, TipoParametro.UrlSegment)
                                    .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorObterDadosCliente, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorObterDadosCliente, EnumMensagemTipo.comunicacaoapi);

                return null;
            }

            return response.RetornoApi<ClienteDto>(_mensagens);
        }

        public async Task<string> ObterAutorizacaoConsultaBeneficioExistente(CPF cpf, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                                    .AddNomeApi(NOME_API_CLIENTE)
                                    .AddUrlApi(_configuracaoProviders.ClienteApi)
                                    .AddUrlMetodo("Beneficio/ObterAutorizacaoValida/{cpf}")
                                    .AddParametro("cpf", cpf.ToString(), TipoParametro.UrlSegment)
                                    .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorObterAutorizacaoValida, request, response);

                await _beneficioInssMensagem.ObterMensagemTratada(response);

                _mensagens.AdicionarErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorObterAutorizacaoValida, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<string>(_mensagens);
        }

        public async Task<string> ObterNovaAutorizacaoParaConsultaBeneficioInss(NovaAutorizacaoParametrosDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                                    .AddNomeApi(NOME_API_CLIENTE)
                                    .AddUrlApi(_configuracaoProviders.ClienteApi)
                                    .AddUrlMetodo("Beneficio/ObterNovaAutorizacao")
                                    .AddBody(parametros)
                                    .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorObterNovaAutorizacao, request, response);

                await _beneficioInssMensagem.ObterMensagemTratada(response);

                _mensagens.AdicionarErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorObterNovaAutorizacao, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<string>(_mensagens);
        }

        public async Task<IEnumerable<ListagemBeneficiosInssDto>> ListarBeneficiosInss(string chaveConsultaBeneficio, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                                    .AddNomeApi(NOME_API_CLIENTE)
                                    .AddUrlApi(_configuracaoProviders.ClienteApi)
                                    .AddUrlMetodo("Beneficio/ListarBeneficios")
                                    .AddParametro("tokenAutorizacao", chaveConsultaBeneficio, TipoParametro.QueryString)
                                    .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorListarBeneficios, request, response);

                await _beneficioInssMensagem.ObterMensagemTratada(response);

                _mensagens.AdicionarErro(Mensagens.ProviderCliente_NaoHouveSucessoNoRetornoDoProvedorListarBeneficios, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<IEnumerable<ListagemBeneficiosInssDto>>(_mensagens);
        }
    }
}
