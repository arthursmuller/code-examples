using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.FeatureFlags
{
    [ExcludeFromCodeCoverage]
    public class FeatureFlagModel
    {
        public bool Habilitado { get; set; }
        public string Chave { get; set; }

        public FeatureFlagModel() {}

        public FeatureFlagModel(string chave, bool habilitado)
        {
            Habilitado = habilitado;
            Chave = chave;
        }
    }
}
