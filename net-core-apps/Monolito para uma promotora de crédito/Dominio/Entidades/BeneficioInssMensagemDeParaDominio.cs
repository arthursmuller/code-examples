namespace Dominio
{
    public class BeneficioInssMensagemDeParaDominio : EntidadeBase
    {
        public string CodigoOriginal { get; private set; }
        public string MensagemOriginal { get; private set; }
        public string MensagemTratada { get; private set; }

        private BeneficioInssMensagemDeParaDominio() { }

        public BeneficioInssMensagemDeParaDominio(string codigoOrignal, string mensagemOriginal, string mensagemTratada)
        {
            CodigoOriginal = codigoOrignal;
            MensagemOriginal = mensagemOriginal;
            MensagemTratada = mensagemTratada;
        }
    }
}
