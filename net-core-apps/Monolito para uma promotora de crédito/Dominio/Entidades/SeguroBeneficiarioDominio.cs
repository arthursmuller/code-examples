using System;

namespace Dominio
{
    public class SeguroBeneficiarioDominio : EntidadeBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal ValorPercentual { get; set; }
        public int? IdSeguroProposta { get; private set; }
        public SeguroPropostaDominio SeguroProposta { get; private set; }
        public int? IdSeguroCliente { get; private set; }
        public SeguroClienteIcatuDominio SeguroCliente { get; private set; }
        public int? IdSeguroParentesco { get; private set; }
        public SeguroParentescoDominio SeguroParentesco { get; private set; }
        public SeguroBeneficiarioDominio() { }
        public SeguroBeneficiarioDominio(string nome, string cPF, DateTime dataNascimento, decimal valorPercentual, int? idSeguroProposta, int? idSeguroCliente, int? idSeguroParentesco)
        {
            Nome = nome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            ValorPercentual = valorPercentual;
            IdSeguroProposta = idSeguroProposta;
            IdSeguroCliente = idSeguroCliente;
            IdSeguroParentesco = idSeguroParentesco;
        }
    }
}
