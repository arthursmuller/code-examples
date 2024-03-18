using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SeguroProfissaoDominio : EntidadeBase
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public SeguroProfissaoDominio(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
    }
}
