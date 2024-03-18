using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Model.SeguroCobertura
{
    public class SeguroCoberturaModel
    {
        public int Id { get; set; }
        public int CodigoCobertura { get; set; }
        public char Tipo { get; set; }
        public decimal ValorCapital { get; set; }
        public decimal ValorPremio { get; set; }
        public char TipoProponente { get; set; }
    }
}
