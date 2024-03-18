using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class TorpedoVozFornecedorDominio: EntidadeBase
    {
        public string NomeExibicao { get; private set; }
        public string ChaveEnvio { get; private set; }
        public int IdEmpresa { get; private set; }

        public EmpresaDominio Empresa { get; private set; }

        public IEnumerable<TorpedoVozMensagemDominio> TorpedoVozMensagens { get; private set; }

        public TorpedoVozFornecedorDominio(string nomeExibicao, int idEmpresa)
        {
            NomeExibicao = nomeExibicao;
            IdEmpresa = idEmpresa;
        }
    }
}
