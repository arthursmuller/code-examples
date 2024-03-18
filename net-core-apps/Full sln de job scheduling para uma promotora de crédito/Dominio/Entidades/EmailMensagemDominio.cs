using System;

namespace Dominio.Entidades
{
    public class EmailMensagemDominio: EntidadeBase
    {
        public string CodigoReferenciaEmail { get; private set; }

        public string Destinatario { get; private set; }

        public string Copia { get; private set; }

        public string Assunto { get; private set; }

        public string Mensagem { get; private set; }

        public bool Prioritario { get; private set; }

        public DateTime DataInsercao { get; private set; }

        public DateTime? DataEnvio { get; private set; }

        public DateTime? DataRecebimento { get; private set; }

        public int IdEmailFornecedor { get; private set; }

        public EmailFornecedorDominio EmailFornecedor { get; private set; }

        public EmailMensagemDominio(string codigoReferenciaEmail, string destinatario, string copia, string assunto, string mensagem, bool prioritario, DateTime dataInsercao, int idEmailFornecedor)
        {
            CodigoReferenciaEmail = codigoReferenciaEmail;
            Destinatario = destinatario;
            Copia = copia;
            Assunto = assunto;
            Mensagem = mensagem;
            Prioritario = prioritario;
            DataInsercao = dataInsercao;
            IdEmailFornecedor = idEmailFornecedor;
        }

        public void RegistrarEnvio()
        {
            DataEnvio = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }

        public void RegistrarRecebimento()
        {
            DataRecebimento = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }
    }
}
