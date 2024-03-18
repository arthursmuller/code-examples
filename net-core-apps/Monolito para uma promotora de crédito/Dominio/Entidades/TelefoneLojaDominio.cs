namespace Dominio
{
    public class TelefoneLojaDominio : EntidadeBase
    {
        public string Telefone { get; private set; }

        public int IdLoja { get; private set; }

        public bool PossuiContaWhatsApp { get; private set; }

        public string MensagemApresentacao { get; private set; }

        public LojaDominio Loja { get; private set; }

        public TelefoneLojaDominio() { }

        public TelefoneLojaDominio(string telefone, bool possuiContaWhatsApp, string mensagemApresentacao)
        {
            Telefone = telefone;
            PossuiContaWhatsApp = possuiContaWhatsApp;
            MensagemApresentacao = mensagemApresentacao;
        }
    }
}
