namespace Infraestrutura.Providers.Consignado.Dto
{
    public class RetornoContratoClienteDto
    {
        public string ClienteCodigo { get; set; }

        public string BancoContrato { get; set; }

        public string ContratoCodigo { get; set; }

        public string Conveniada { get; set; }

        public string Orgao { get; set; }

        public string ConveniadaDescricao { get; set; }

        public string OrgaoDescricao { get; set; }

        public decimal PMTOriginal { get; set; }

        public string Matricula { get; set; }

        public int PrazoCodigo { get; set; }

        public int ParcelasPagas { get; set; }

        public decimal ValorQuitacao { get; set; }

        public decimal Taxa { get; set; }

        public decimal TaxaMes { get; set; }

        public string CodigoOperacao { get; set; }

        public string TipoOperacaoDetalhado { get; set; }

        public bool Refinanciavel { get; set; }

        public string MensagemRefin { get; set; }
    }
}
