using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SeguroProfissaoIcatuDominio : EntidadeBase
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdSeguroProfissaoBem { get; private set; }
        public SeguroProfissaoDominio SeguroProfissaoBem { get; private set; }

        public SeguroProfissaoIcatuDominio() { }

        public SeguroProfissaoIcatuDominio(int codigo, string descricao, int idSeguroProfissaoBem)
        {
            Codigo = codigo;
            Descricao = descricao;
            IdSeguroProfissaoBem = idSeguroProfissaoBem;
        }
    }
}
