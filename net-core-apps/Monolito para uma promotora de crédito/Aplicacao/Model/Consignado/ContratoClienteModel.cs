namespace Aplicacao.Model.Consignado
{
    public class ContratoClienteModel
    {
        public string Matricula { get; set; }

        public string Contrato { get; set; }

        public decimal Prestacao { get; set; }

        public int QtdParcelas { get; set; }

        public int QtdParcelasPagas { get; set; }

        public decimal Taxa { get; set; }

        public decimal SaldoTotal { get; set; }
    }
}
