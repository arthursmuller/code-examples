using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class SeguroParentescoIcatuDominio : EntidadeBase
    {
        public string Descricao { get; set; }
        public int Codigo { get; set; }

        public int IdSeguroParentescoBem { get; private set; }
        public SeguroParentescoBemDominio SeguroParentescoBem { get; private set; }

        public SeguroParentescoIcatuDominio() { }

        public SeguroParentescoIcatuDominio(int codigo, string descricao, int idSeguroParentescoBem)
        {
            Codigo = codigo;
            Descricao = descricao;
            IdSeguroParentescoBem = idSeguroParentescoBem;
        }

    }
}
