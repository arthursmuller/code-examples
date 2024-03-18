using System;

namespace Aplicacao.Model.Biometria
{

    public class DadosRetornoUnico
    {
        public string id { get; set; }
        public int status { get; set; }
        public int score { get; set; }
    }

    public class BiometriaWebhookRetornoUnicoModel
    {
        public DateTime eventDate { get; set; }
        public string @event { get; set; }
        public DadosRetornoUnico data { get; set; }
    }
}
