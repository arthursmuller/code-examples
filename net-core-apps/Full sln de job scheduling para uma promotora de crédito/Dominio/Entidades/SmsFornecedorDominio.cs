using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class SmsFornecedorDominio: EntidadeBase
    {
        public string NomeExibicao { get; private set; }
        
        public string Usuario { get; private set; }

        public string Senha { get; private set; }

        public int CodigoAgrupador { get; private set; }

        public int IdEmpresa { get; private set; }

        public EmpresaDominio Empresa { get; private set; }

        public IEnumerable<SmsMensagemDominio> SmsMensagens { get; private set; }

        public string CredenciaisBase64 { get => Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{Usuario}:{Senha}")); }

        public SmsFornecedorDominio(string nomeExibicao, string usuario, string senha, int codigoAgrupador, int idEmpresa)
        {
            NomeExibicao = nomeExibicao;
            Usuario = usuario;
            Senha = senha;
            CodigoAgrupador = codigoAgrupador;
            IdEmpresa = idEmpresa;
        }
    }
}
