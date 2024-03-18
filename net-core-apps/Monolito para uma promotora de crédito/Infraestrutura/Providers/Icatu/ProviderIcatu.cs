using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Mensagens.Interfaces;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.IcatuApi.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.IcatuApi
{
    [ExcludeFromCodeCoverage]
    public class ProviderIcatu : ProviderBase, IProviderIcatu
    {
        private const string NOME_API = "IcatuApi";
        private readonly string _propostaNumeroUrl = "/relacionamento-parceiro/vida/venda/v2/propostas-numero";
        private readonly string _meioPagamentosUrl = "/meio-pagamento/v1/pedidos";
        private readonly string _propostaUrl = "/relacionamento-parceiro/vida/venda/v2/propostas";
        private readonly IcatuConfiguracao _icatuConfiguracao;
        public ProviderIcatu(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderIcatu> logger, IcatuConfiguracao icatuConfiguracao)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) 
            => _icatuConfiguracao = icatuConfiguracao;

        public async Task<ConsultarPedidoPagamentoDto> ConsultarPedidoPagamento(string idPedidoPagamento)
        {
            var request = _conecta.Get()
                .AddNomeApi(NOME_API)
                .AddUrlApi(_icatuConfiguracao.BaseUrl)
                .AddHeader("Ocp-Apim-Subscription-Key", _icatuConfiguracao.OcpApimSubscriptionKey)
                .AddHeader("LinhaNegocio", _icatuConfiguracao.LinhaNegocio)
                .AddHeader("Empresa", _icatuConfiguracao.Empresa)
                .AddUrlMetodo($"{_meioPagamentosUrl}/{idPedidoPagamento}");

            var resposta = await _clienteConecta.Executar(request);

            if (resposta.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ConsultarPedidoPagamentoDto>(resposta.Content);

            _mensagens.AdicionarErro(resposta.Content);
            return null;
        }

        public async Task<CriarNumeroPropostaRespostaDto> CriarNumeroProposta(CriarNumeroPropostaDto model)
        {
            var request = _conecta.Post()
                .AddNomeApi(NOME_API)
                .AddUrlApi(_icatuConfiguracao.BaseUrl)
                .AddBody(model)
                .AddHeader("CodigoEmpresa", _icatuConfiguracao.CodigoEmpresa)
                .AddHeader("Ocp-Apim-Subscription-Key", _icatuConfiguracao.OcpApimSubscriptionKey)
                .AddUrlMetodo(_propostaNumeroUrl);

            var resposta = await _clienteConecta.Executar(request);

            if (resposta.StatusCode == HttpStatusCode.Created)
                return JsonConvert.DeserializeObject<CriarNumeroPropostaRespostaDto>(resposta.Content);

            _mensagens.AdicionarErro(resposta.Content);
            return null;
        }

     public async Task<PedidoPagamentoRespostaDto> CriarPedidoPagamento(CriarPedidoPagamentoDto model)
        {
            var jsonData = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd"
            });

            var request = _conecta.Post()
                .AddNomeApi(NOME_API)
                .AddUrlApi(_icatuConfiguracao.BaseUrl)
                .AddJsonBody(jsonData)
                .AddHeader("CodigoEmpresa", _icatuConfiguracao.CodigoEmpresa)
                .AddHeader("Ocp-Apim-Subscription-Key", _icatuConfiguracao.OcpApimSubscriptionKey)
                .AddHeader("Empresa", _icatuConfiguracao.Empresa)
                .AddHeader("LinhaNegocio", _icatuConfiguracao.LinhaNegocio)
                .AddUrlMetodo(_meioPagamentosUrl);

            var resposta = await _clienteConecta.Executar(request);

            if (resposta.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<PedidoPagamentoRespostaDto>(resposta.Content);

            _mensagens.AdicionarErro(resposta.Content);
            return null;
        }
        
        public async Task<CriarPropostaRespostaDto> CriarProposta(CriarPropostaDto model)
        {
            var jsonData = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,

                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),

                DateFormatString = "yyyy-MM-dd"
            });

            var request = _conecta.Post()
                .AddNomeApi(NOME_API)
                .AddUrlApi(_icatuConfiguracao.BaseUrl)
                .AddJsonBody(jsonData)
                .AddHeader("CodigoEmpresa", _icatuConfiguracao.CodigoEmpresa)
                .AddHeader("Ocp-Apim-Subscription-Key", _icatuConfiguracao.OcpApimSubscriptionKey)
                .AddUrlMetodo(_propostaUrl);

            var resposta = await _clienteConecta.Executar(request);

            if (resposta.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<CriarPropostaRespostaDto>(resposta.Content);
            
            _mensagens.AdicionarErro(resposta.Content);
            return null;
        }

    }
}
