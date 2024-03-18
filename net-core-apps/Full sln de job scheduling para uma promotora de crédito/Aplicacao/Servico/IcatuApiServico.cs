using Dominio.Abstracoes;
using Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class IcatuApiServico : IIcatuApiServico
    {
        private readonly HttpClient _httpClient;
        private readonly string _parentescoUrl = "/comum/v1/parentescos";
        private readonly string _profissaoUrl = "/comum/v1/profissoes";

        public IcatuApiServico(HttpClient httpClient) => 
            _httpClient = httpClient;

        public async Task<IEnumerable<IcatuParentescoDto>> GetParentescos() 
            => await deserializarResposta<IcatuParentescoDto>(await get(_parentescoUrl));
        
        public async Task<IEnumerable<IcatuProfissaoDto>> GetProfissoes() 
            => await deserializarResposta<IcatuProfissaoDto>(await get(_profissaoUrl));

        private async Task<HttpResponseMessage> get(string endpoint) => 
            await _httpClient.GetAsync(endpoint);
 
        private async Task<IEnumerable<T>> deserializarResposta<T>(HttpResponseMessage response)
            => response.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync
                    <IEnumerable<T>>(await response.Content.ReadAsStreamAsync()) 
                : Array.Empty<T>();
    }
}
