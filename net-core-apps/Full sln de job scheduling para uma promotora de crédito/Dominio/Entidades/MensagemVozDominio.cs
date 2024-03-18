using System;

namespace Dominio.Entidades
{
    public class MensagemVozDomimio: EntidadeBase
    {
        public string CodigoReferenciaMensagem { get; private set; }

        public string NumeroTelefone { get; private set; }

        public string Mensagem { get; private set; }

        public bool Situacao { get; private set; }

        public DateTime DataInsercao { get; private set; }


        public MensagemVozDomimio(string codigoReferenciaMensagem, string numeroTelefone, string mensagem, DateTime dataInsercao)
        {
            CodigoReferenciaMensagem = codigoReferenciaMensagem;
            NumeroTelefone = numeroTelefone;
            Mensagem = mensagem;
            DataInsercao = dataInsercao;
            Situacao = false;
        }


        public void InformarSituacaoEnvio(bool status){

            DataAtualizacao = DateTime.Now;
            Situacao = status;
        }
    }
}
