using System;

namespace Dominio.Entidades
{
    public class TorpedoVozMensagemDominio: EntidadeBase
    {
        public string CodigoReferenciaMensagem { get; private set; }

        public string NumeroTelefone { get; private set; }

        public string Mensagem { get; private set; }

        public bool Situacao { get; private set; }

        public DateTime DataInsercao { get; private set; }

        public int IdTorpedoVozFornecedor { get; private set; }

        public TorpedoVozFornecedorDominio TorpedoVozFornecedor { get; private set; }

        public TorpedoVozMensagemDominio( string codigoReferenciaMensagem
                                        , string numeroTelefone
                                        , string mensagem
                                        , int idTorpedoVozFornecedor
                                        , DateTime dataInsercao)
        {
            CodigoReferenciaMensagem = codigoReferenciaMensagem;
            NumeroTelefone = numeroTelefone;
            Mensagem = mensagem;
            DataInsercao = dataInsercao;
            IdTorpedoVozFornecedor = idTorpedoVozFornecedor;
            Situacao = false;
        }


        public void InformarSituacaoEnvio(bool status){
            DataAtualizacao = DateTime.Now;
            Situacao = status;
        }
    }
}
