using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SeguroCoberturaDominio : EntidadeBase
    {
        public int CodigoCobertura { get; set; }
        public char Tipo { get; set; }
        public decimal ValorCapital { get; set; }
        public decimal ValorPremio { get; set; }
        public char TipoProponente { get; set; }
        public int IdSeguroProduto { get; private set; }
        public SeguroProdutoDominio SeguroProduto { get; private set; }
        public SeguroCoberturaDominio() { }
        public SeguroCoberturaDominio(int codigoCobertura, char tipo, decimal valorCapital, decimal valorPremio, char tipoProponente, int idSeguroProduto)
        {
            CodigoCobertura = codigoCobertura;
            Tipo = tipo;
            ValorCapital = valorCapital;
            ValorPremio = valorPremio;
            TipoProponente = tipoProponente;
            IdSeguroProduto = idSeguroProduto;
        }
    }
}
