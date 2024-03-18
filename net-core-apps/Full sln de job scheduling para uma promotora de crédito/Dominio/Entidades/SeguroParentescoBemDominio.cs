using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class SeguroParentescoBemDominio : EntidadeBase
    {
        public string Descricao { get; set; }
        public int Codigo { get; set; }

        public SeguroParentescoIcatuDominio SeguroParentescoIcatu { get; private set; }

        public SeguroParentescoBemDominio() { }

        public SeguroParentescoBemDominio(string descricao, SeguroParentescoIcatuDominio seguroParentescoIcatu)
        {
            Descricao = descricao;
            SeguroParentescoIcatu = seguroParentescoIcatu;
        }
    }
}
