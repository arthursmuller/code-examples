using System.Diagnostics.CodeAnalysis;

namespace Aplicacao
{
    [ExcludeFromCodeCoverage]
    public class LeadAtualizadaModel
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string LinkContatoWhatsAppLoja { get; set; }
    }
}
