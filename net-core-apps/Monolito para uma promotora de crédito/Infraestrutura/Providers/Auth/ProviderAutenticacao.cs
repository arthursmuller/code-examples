using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Dto;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Auth
{
    [ExcludeFromCodeCoverage]
    public class ProviderAutenticacao : ProviderBase, IProviderAutenticacao
    {
        public ProviderAutenticacao(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderAutenticacao> logger)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) { }

        public async Task<RetornoAtenticacaoDto> Autenticar(ParametroAutenticacaoDto parametros)
        {
            var request = _conecta.Post()
                .AddNomeApi("AuthApi")
                .AddUrlApi(_configuracaoProviders.AuthApi)
                .AddUrlMetodo("Autenticacao/Autenticar")
                .AddBody(parametros);

            var response = await _clienteConecta.Executar(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AdicionarLogErro(Mensagens.ProviderAutenticacao_NaoHouveSucessoNoRetornoDoProvedorAutenticacao, request, response);

                _mensagens.AdicionarErro(Mensagens.ProviderAutenticacao_NaoHouveSucessoNoRetornoDoProvedorAutenticacao, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return response.RetornoApi<RetornoAtenticacaoDto>(_mensagens);
        }
    }
}
