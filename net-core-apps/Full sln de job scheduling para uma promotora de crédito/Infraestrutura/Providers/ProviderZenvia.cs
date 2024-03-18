using B.Mensagens.Interfaces;
using System.Threading.Tasks;
using Infraestrutura.Enum;
using B.Mensagens;
using Infraestrutura.DTO.Zenvia;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers
{
    [ExcludeFromCodeCoverage]
    public class ProviderZenvia : IProviderZenvia
    {
        private readonly IBemMensagens _mensageria;

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public ProviderZenvia(IBemMensagens mensageria)
        {
            _mensageria = mensageria;
        }

        public async Task<(StatusEnvio, ZenviaStatus?, ZenviaStatusDetalhes?)> EnviarMensagem(ZenviaSmsMensagemDto requisicao, string credenciais)
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api-rest.zenvia.com/services/send-sms")) 
                {
                    httpRequest.Headers.Add("Accept", "application/json");
                    httpRequest.Headers.Add("Authorization", $"Basic {credenciais}");

                    var json = JsonConvert.SerializeObject(new { sendSmsRequest = requisicao }, serializerSettings);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                    httpRequest.Content = data;

                    var retorno = await httpClient.SendAsync(httpRequest);

                    if (retorno.IsSuccessStatusCode)
                    {
                        var retornoFormatado = (JsonConvert.DeserializeObject<ZenviaSmsMensagemRespostaDto>(await retorno.Content.ReadAsStringAsync(), serializerSettings)).sendSmsResponse;
                        var status = retornoFormatado.StatusCode;

                        if (status != ZenviaStatus.Ok)
                        {
                            _mensageria.AdicionarErro($"Requisição inválida: {status} - {retornoFormatado.StatusDescription}", EnumMensagemTipo.formulario);
                            _mensageria.AdicionarErro($"Detalhes: {retornoFormatado.DetailCode} - {retornoFormatado.DetailDescription}", EnumMensagemTipo.formulario);
                        }

                        return (StatusEnvio.Sucesso, status, retornoFormatado.DetailCode);
                    }
                    else 
                    {
                        var mensagem = await retorno.Content.ReadAsStringAsync();
                        _mensageria.AdicionarErro($"Requisição para o serviço inválida: {retorno.StatusCode} - {mensagem}.", EnumMensagemTipo.comunicacaoapi);
                        return (StatusEnvio.Erro, null, null);
                    }
                }
            }
        }

        public async Task<(StatusEnvio, ZenviaStatus?, ZenviaStatusDetalhes?, string)> ConsultarRequisicao(string id, string credenciais)
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api-rest.zenvia.com/services/get-sms-status/{id}")) 
                {
                    httpRequest.Headers.Add("Accept", "application/json");
                    httpRequest.Headers.Add("Authorization", $"Basic {credenciais}");

                    var retorno = await httpClient.SendAsync(httpRequest);

                    if (retorno.IsSuccessStatusCode)
                    {
                        var retornoFormatado = (JsonConvert.DeserializeObject<ZenviaSmsMensagemStatusDto>(await retorno.Content.ReadAsStringAsync(), serializerSettings)).getSmsStatusResp;

                        return (StatusEnvio.Sucesso, retornoFormatado.StatusCode, retornoFormatado.DetailCode, retornoFormatado.MobileOperatorName);
                    }
                    else 
                    {
                        var mensagem = await retorno.Content.ReadAsStringAsync();
                        _mensageria.AdicionarErro($"Requisição para o serviço inválida: {retorno.StatusCode} - {mensagem}.", EnumMensagemTipo.comunicacaoapi);
                        return (StatusEnvio.Erro, null, null, null);
                    }
                }
            }
        }
    }
}
