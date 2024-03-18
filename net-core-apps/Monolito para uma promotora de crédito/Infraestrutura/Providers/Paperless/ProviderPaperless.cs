using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.Paperless.Dto;
using Infraestrutura.Providers.Paperless.Dto.AssinaturaDocumento;
using Infraestrutura.Providers.Paperless.Dto.ReenvioToken;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Paperless
{
    [ExcludeFromCodeCoverage]
    public class ProviderPaperless : ProviderBase, IProviderPaperless
    {
        private const string NOME_API_PAPERLESS = "Paperless";

        public ProviderPaperless(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderPaperless> logger)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) { }

        public async Task<int?> EnviarDocumentoParaAssinatura(InformacoesEnvioDocumentoParaAssinaturaDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                                      .AddNomeApi(NOME_API_PAPERLESS)
                                      .AddUrlApi(_configuracaoProviders.Paperless.PaperlessApi)
                                      .AddUrlMetodo("Paperless/EnviarDocumentoParaAssinatura")
                                      .AddBody(parametros)
                                      .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderPaperless_NaoHouveSucessoNoRetornoDoProvedorEnviarDocumentoParaAssinatura, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderPaperless_NaoHouveSucessoNoRetornoDoProvedorEnviarDocumentoParaAssinatura, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<int?>(_mensagens);
        }

        public async Task<byte[]> AssinarDocumento(AssinaturaDocumentoDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                                      .AddNomeApi(NOME_API_PAPERLESS)
                                      .AddUrlApi(_configuracaoProviders.Paperless.PaperlessApi)
                                      .AddUrlMetodo("Paperless/AssinarDocumento")
                                      .AddBody(parametros)
                                      .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderPaperless_NaoHouveSucessoNoRetornoDoProvedorAssinaturaDocumento, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderPaperless_NaoHouveSucessoNoRetornoDoProvedorAssinaturaDocumento, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<byte[]>(_mensagens);
        }

        public async Task<bool> ReenviarToken(ReenvioTokenParametrosDto parametros, string tokenAutenticacao)
        {
            var request = _conecta.Post()
                                      .AddNomeApi(NOME_API_PAPERLESS)
                                      .AddUrlApi(_configuracaoProviders.Paperless.PaperlessApi)
                                      .AddUrlMetodo("Paperless/ReenviarToken")
                                      .AddBody(parametros)
                                      .AddTokenTemporaria(tokenAutenticacao);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderPaperless_NaoHouveSucessoNoRetornoDoProvedorReenviarToken, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderPaperless_NaoHouveSucessoNoRetornoDoProvedorReenviarToken, EnumMensagemTipo.comunicacaoapi);
                return false;
            }

            return response.RetornoApi<bool>(_mensagens);
        }
    }
}
