using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Comunicacao.Models;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Infraestrutura.Providers.BemApi.Dto;
using Infraestrutura.Providers.Dto;
using Microsoft.Extensions.Logging;
using SharedKernel.ValueObjects.v2;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.BemApi
{
    [ExcludeFromCodeCoverage]
    public class ProviderBemApi : ProviderBase, IProviderBemApi
    {
        private const string NOME_API = "BemApi";

        public ProviderBemApi(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderBemApi> logger)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) { }

        public async Task<ObtencaoSituacaoPropostaDto> ObterSituacaoProposta(CPF cpf, string token, DateTime dataNascimento, string tokenAutenticacao)
        {
            var request = _conecta.Get()
                                    .AddNomeApi(NOME_API)
                                    .AddUrlApi(_configuracaoProviders.BemApi)
                                    .AddUrlMetodo("clientes/{cpf}/consultas-proposta/{senha}/situacao")
                                    .AddParametro("cpf", cpf.ToString(), TipoParametro.UrlSegment)
                                    .AddParametro("senha", token, TipoParametro.UrlSegment)
                                    .AddParametro("dataNascimento", dataNascimento, TipoParametro.QueryString)
                                    .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderBemApi_NaoHouveSucessoNoRetornoDoProvedorDeConsultaDaSituacaoDaProposta, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderBemApi_NaoHouveSucessoNoRetornoDoProvedorDeConsultaDaSituacaoDaProposta, EnumMensagemTipo.comunicacaoapi);
            }

            return response.RetornoApi<ObtencaoSituacaoPropostaDto>(_mensagens);
        }
    }
}
