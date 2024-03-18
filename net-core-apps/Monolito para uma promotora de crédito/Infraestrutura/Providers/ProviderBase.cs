using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Mensagens.Interfaces;
using Infraestrutura.Providers.Dto;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infraestrutura.Providers
{
    public class ProviderBase
    {
        protected readonly IConecta _conecta;
        protected readonly IBemMensagens _mensagens;
        protected readonly ConfiguracaoProviders _configuracaoProviders;
        protected readonly ILogger<ProviderBase> _logger;
        protected readonly IClienteConecta _clienteConecta;

        public ProviderBase(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderBase> logger)
        {
            _conecta = conecta;
            _mensagens = mensagens;
            _configuracaoProviders = configuracaoProviders;
            _logger = logger;
            _clienteConecta = clienteConecta;
        }

        protected void AdicionarLogErro(string menssagemTratada, IConectaRequest request, IConectaResponse response)
            => _logger.LogError($"Mensagem tratada: {menssagemTratada} Status code retorno: [{(int)response.StatusCode}] {response.StatusCode}. Payload: {montarPayload(request)} Retorno provider: {response.RetornoApi()}");

        private string montarPayload(IConectaRequest request)
            => $"Url: {request.UrlApi}/{request.UrlMetodo} Parâmetros: {JsonSerializer.Serialize(request.Parametros)} Body: {JsonSerializer.Serialize(request.Body)}";
    }
}
