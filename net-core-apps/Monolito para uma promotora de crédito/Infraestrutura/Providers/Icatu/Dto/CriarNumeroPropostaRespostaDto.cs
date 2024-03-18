using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]
    public class CriarNumeroPropostaResposta
    {
        public string Proposta { get; set; }
        public string Apolice { get; set; }
        public string CertificadoApolice { get; set; }
        public string LinhaDigitavel { get; set; }
        public string CodigoBarras { get; set; }
        public DateTime DataVencimento { get; set; }
        public int FormaPagamento { get; set; }
        public int GrupoApolice { get; set; }
        public int Subestipulante { get; set; }
        public int Modulo { get; set; }
        public string NomeParceiro { get; set; }
        public string NossoNumero { get; set; }
        public string Banco { get; set; }
        public string Convenio { get; set; }
        public int Parceiro { get; set; }
        public int Produto { get; set; }
        public string TituloCapitalizacao { get; set; }
    }

    public class CriarNumeroPropostaRespostaDto
    {
        public CriarNumeroPropostaResposta NumeracaoProposta { get; set; }

    }
}
