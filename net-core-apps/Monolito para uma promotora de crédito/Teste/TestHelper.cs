using System.Collections.Generic;

namespace Teste
{
    public class TestHelper
    {
        public static Dictionary<string, double[]> ObterGeolocalizacoes()
        {
            var dicionarioGeolocalizacoes = new Dictionary<string, double[]>();
            dicionarioGeolocalizacoes.Add("centro", new double[] { -30.030834, -51.232420 });
            dicionarioGeolocalizacoes.Add("moinhos", new double[] { -30.025244, -51.202721 });
            dicionarioGeolocalizacoes.Add("sapucaia", new double[] { -29.828714, -51.144189 });
            dicionarioGeolocalizacoes.Add("cidreira", new double[] { -30.176527, -50.209334 });
            dicionarioGeolocalizacoes.Add("criciuma", new double[] { -28.677395, -49.366001 });

            return dicionarioGeolocalizacoes;
        }
    }
}
