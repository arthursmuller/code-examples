using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class EmailFornecedorDominio: EntidadeBase
    {
        public string NomeExibicao { get; private set; }
        
        public string Usuario { get; private set; }

        public string Senha { get; private set; }

        public string Host { get; private set; }

        public int Porta { get; private set; }

        public bool Ssl { get; private set; }

        public int IdEmpresa { get; private set; }

        public EmpresaDominio Empresa { get; private set; }

        public IEnumerable<EmailMensagemDominio> EmailMensagens { get; private set; }

        public EmailFornecedorDominio(string nomeExibicao, string usuario, string senha, string host, int porta, bool ssl, int idEmpresa)
        {
            NomeExibicao = nomeExibicao;
            Usuario = usuario;
            Senha = senha;
            Host = host;
            Porta = porta;
            Ssl = ssl;
            IdEmpresa = idEmpresa;
        }
    }
}
