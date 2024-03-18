using Dominio;
using Dominio.Resource;
using MaxMind.GeoIP2;
using Microsoft.Extensions.Logging;
using System;

namespace Infraestrutura
{
    public class ProviderMaxMind : IProviderMaxMind
    {
        private readonly WebServiceClient _maxMindClient;
        private readonly ILogger<ProviderMaxMind> _logger;
        private readonly IUsuarioLogin _usuarioLogin;

        public ProviderMaxMind(WebServiceClient maxMindClient, ILogger<ProviderMaxMind> logger, IUsuarioLogin usuarioLogin)
        {
            _maxMindClient = maxMindClient;
            _logger = logger;
            _usuarioLogin = usuarioLogin;
        }

        public (double? latitude, double? longitude) ObterLatitudeLongitude()
        {
            try
            {
                var response = _maxMindClient.City(_usuarioLogin.EnderecoIpOrigemRequisicao);

                if (PossuiLocalizacao(response))
                    return (response.Location.Latitude, response.Location.Longitude);

                return (0, 0);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Mensagens.ProviderMaxMind_OcorreuErroAoTentarObterGeolocalizacao} { ex.Message}.");

                return (0, 0);
            }
        }

        private static bool PossuiLocalizacao(MaxMind.GeoIP2.Responses.CityResponse response)
        {
            return response.Location != null && response.Location.Latitude != null && response.Location.Longitude != null;
        }
    }
}