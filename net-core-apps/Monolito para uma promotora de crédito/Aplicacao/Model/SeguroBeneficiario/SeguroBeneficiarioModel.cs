using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Model.SeguroBeneficiario
{
    public class SeguroBeneficiarioModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal ValorPercentual { get; set; }
        public int? IdSeguroParentesco { get; set; }
    }
}
