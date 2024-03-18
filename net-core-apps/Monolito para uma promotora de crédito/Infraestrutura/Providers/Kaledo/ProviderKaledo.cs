using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Mensagens.Interfaces;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.Kaledo.DTO;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using B.Mensagens;
using Dominio.Resource;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Kaledo
{
    [ExcludeFromCodeCoverage]
    public class ProviderKaledo : ProviderBase, IProviderKaledo
    {
        private const string NOME_API = "Kaledo";
        private readonly KaledoConfiguracao _kaledoConfiguracao;

        public ProviderKaledo(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderKaledo> logger, KaledoConfiguracao kaledoConfiguracao) 
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) => _kaledoConfiguracao = kaledoConfiguracao;

        public async Task<KaledoResultadoCriarAuntenticarUsuarioDTO> CriarAutenticarUsuario(KaledoCriarAutenticarUsuarioDTO dto)
        {
            var conectaRequisicao = _conecta
                .Post()
                .AddBody(dto)
                .AddNomeApi(NOME_API)
                .AddUrlApi(_kaledoConfiguracao.UrlBaseApi)
                .AddUrlMetodo($"{_kaledoConfiguracao.UrlCriarAutenticarUsuario}/{_kaledoConfiguracao.ChaveApi}");

            var resposta = await _clienteConecta.Executar(conectaRequisicao);

            if (resposta.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<KaledoResultadoCriarAuntenticarUsuarioDTO>(resposta.Content);

            _mensagens.AdicionarErro(Mensagens.ProviderKaledo_NaoHouveSucessoNoRetornoDoProvedorCriarAuntenticarUsuario, EnumMensagemTipo.formulario);
            return null;
        }
    }
}
