using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class SeguroProfissaoIcatuDominio : EntidadeBase
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public int IdSeguroProfissaoBem { get; private set; }
        public SeguroProfissaoBemDominio SeguroProfissaoBem { get; private set; }

        public SeguroProfissaoIcatuDominio() { }

        public SeguroProfissaoIcatuDominio(int codigo, string descricao, int idSeguroProfissaoBem)
        {
            Codigo = codigo;
            Descricao = descricao;
            IdSeguroProfissaoBem = idSeguroProfissaoBem;
        }
    }
}
