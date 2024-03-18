using System;

namespace Aplicacao.Model.Beneficio
{
    public class ConsultaBeneficioModel
    {
        public string NumeroBeneficio { get; set; }
        public int Especie { get; set; }
        public string InstituicaoFinanceira { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string UfRendimento { get; set; }
        public DateTime? DataInscricao { get; set; }
        public decimal MargemDisponivel { get; set; }
        public decimal MargemDisponivelCartao { get; set; }
        public int TipoConta { get; set; }
    }
}
