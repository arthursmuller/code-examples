using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class MarinhaCargoDominio : EntidadeBase
    {
        public string Descricao { get; private set; }
        public string Sigla { get; private set; }
        public int Codigo { get; set; }

        public MarinhaCargoDominio(int codigo, string sigla, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
            Sigla = sigla;
        }
    }
}
