using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class SeguroProfissaoBemDominio : EntidadeBase
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public SeguroProfissaoIcatuDominio SeguroProfissaoIcatu { get; private set; }
    }
}
