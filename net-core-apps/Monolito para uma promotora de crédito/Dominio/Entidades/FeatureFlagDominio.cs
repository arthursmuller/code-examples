namespace Dominio
{
    public class FeatureFlagDominio : EntidadeBase
    {
        private string _chave;
        public string Chave { get => _chave; private set => _chave = value?.Trim().ToUpper(); }

        public bool Habilitado { get; private set; }

        public FeatureFlagDominio() { }

        public FeatureFlagDominio(
            string chave,
            bool valor
        )
        {
            Chave = chave;
            Habilitado = valor;
        }

        public void atualizarHabilitado(bool habilitado)
        {
            this.Habilitado = habilitado;
        }
    }
}