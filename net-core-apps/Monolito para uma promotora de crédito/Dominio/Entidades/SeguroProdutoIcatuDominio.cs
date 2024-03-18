using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SeguroProdutoIcatuDominio : EntidadeBase
    {
        public int CodigoGrupoApolice { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public int Modulo { get; set; }
        public int Subestipulante { get; set; }
        public string CodigoPontoVenda { get; set; }
        public int IdParceria { get; set; }
        public decimal ValorPremio { get; set; }
        public int TipoNumeracaoProposta { get; set; }
        public int? IdSeguroProduto { get; private set; }
        public SeguroProdutoDominio SeguroProduto { get; private set; }

        public SeguroProdutoIcatuDominio(int codigoGrupoApolice, DateTime dataInicioVigencia, DateTime dataFimVigencia, int modulo, int? idSeguroProduto, int subestipulante, string codigoPontoVenda, int idParceria, int tipoNumeracaoProposta, decimal valorPremio)
        {
            CodigoGrupoApolice = codigoGrupoApolice;
            DataInicioVigencia = dataInicioVigencia;
            DataFimVigencia = dataFimVigencia;
            Modulo = modulo;
            IdSeguroProduto = idSeguroProduto;
            Subestipulante = subestipulante;
            CodigoPontoVenda = codigoPontoVenda;
            IdParceria = idParceria;
            TipoNumeracaoProposta = tipoNumeracaoProposta;
            ValorPremio = valorPremio;
        }
    }
}
