using B.Mensagens.Interfaces;
using System.Threading.Tasks;
using Infraestrutura.Enum;
using B.Mensagens;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using System;
using Infraestrutura.DTO.ZenviaTorpedoVoz;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers
{
    [ExcludeFromCodeCoverage]
    public class ProviderZenviaTotalVoice : IProviderZenviaTotalVoice
    {
        private readonly IBemMensagens _mensageria;
        
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public ProviderZenviaTotalVoice(IBemMensagens mensageria)
        {
            _mensageria = mensageria;
        }

        public async Task<bool> EnviarMensagemVoz(ZenviaTorpedoVozDto requisicao, string _credencialApi)
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://voice-api.zenvia.com/tts")) 
                {
                    httpRequest.Headers.Add("Accept", "application/json");
                    httpRequest.Headers.Add("Access-Token", _credencialApi);

                    var json = JsonConvert.SerializeObject( requisicao, serializerSettings);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                    httpRequest.Content = data;

                    var retorno = await httpClient.SendAsync(httpRequest);

                    if (retorno.IsSuccessStatusCode)
                    {
                        var retornoFormatado = (JsonConvert.DeserializeObject<ZenviaTorpedoVozRespostaDto>(await retorno.Content.ReadAsStringAsync(), serializerSettings));
                        var sucesso = retornoFormatado.Sucesso;

                        if (!sucesso)
                        {
                            _mensageria.AdicionarErro($"Requisição inválida: {sucesso}", EnumMensagemTipo.formulario);
                            _mensageria.AdicionarErro($"Detalhes: {retornoFormatado.Motivo} - {retornoFormatado.Mensagem}", EnumMensagemTipo.formulario);
                        }

                        return (sucesso);
                    }
                    else 
                    {
                        var mensagem = await retorno.Content.ReadAsStringAsync();
                        _mensageria.AdicionarErro($"Requisição para o serviço inválida: {retorno.StatusCode} - {mensagem}.", EnumMensagemTipo.comunicacaoapi);
                        return (false);
                    }
                }
            }
        }
    }
}
