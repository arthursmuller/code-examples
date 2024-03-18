using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LoginSocialFacebook
{
    [ExcludeFromCodeCoverage]
    public class ProvedorFacebook : IProvedorFacebook
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConfiguracaoLoginSocialFacebook _configuracaoLoginFacebook;

        public ProvedorFacebook(IHttpClientFactory httpClientFactory, ConfiguracaoLoginSocialFacebook configuracaoLoginFacebook)
        {
            _httpClientFactory = httpClientFactory;
            _configuracaoLoginFacebook = configuracaoLoginFacebook;
        }

        public async Task<ValidacaoTokenRetornoDto> ValidarToken(string token)
        {
            var httpClient = _httpClientFactory.CreateClient("Facebook");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var resposta = await httpClient.GetAsync($"{_configuracaoLoginFacebook.UrlApi}/me?access_token={token}&fields=name,email");

            if (resposta.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException($"Não houve sucesso na validação do token de login do Facebook. Retorno: {await resposta.Content.ReadAsStringAsync()}");

            return await resposta.Content.ReadAsAsync<ValidacaoTokenRetornoDto>();
        }
    }
}
