using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class WhatsappFornecedorDominio : EntidadeBase
    {
        public string NomeExibicao { get; private set; }
        public string Chave { get; private set; }
        public int IdEmpresa { get; private set; }
        public EmpresaDominio Empresa { get; private set; }
        public IEnumerable<WhatsappMensagemDominio> WhatsappMensagens { get; private set; }
        public WhatsappFornecedorDominio(string nomeExibicao, int idEmpresa)
        {
            NomeExibicao = nomeExibicao;
            IdEmpresa = idEmpresa;
        }

        public void defineChave(string chave){
            this.Chave = chave;
            this.DataAtualizacao= DateTime.Now;
        }
    }
}