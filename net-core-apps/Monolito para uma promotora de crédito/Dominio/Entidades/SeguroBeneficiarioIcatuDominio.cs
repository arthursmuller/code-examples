using System;

namespace Dominio
{
    public class SeguroBeneficiarioIcatuDominio : EntidadeBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal ValorPercentual { get; set; }
        public int? IdSeguroProposta { get; private set; }
        public SeguroPropostaIcatuDominio SeguroPropostaIcatu { get; private set; }
        public int? IdSeguroClienteIcatu { get; private set; }
        public SeguroClienteIcatuDominio SeguroClienteIcatu { get; private set; }
        public int? IdSeguroParentescoIcatu { get; private set; }
        public SeguroParentescoIcatuDominio SeguroParentescoIcatu { get; private set; }

        public SeguroBeneficiarioIcatuDominio() { }
        public SeguroBeneficiarioIcatuDominio(string nome, string cPF, DateTime dataNascimento, decimal valorPercentual, int? idSeguroProposta, int? idSeguroClienteIcatu, int? idSeguroParentescoIcatu)
        {
            Nome = nome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            ValorPercentual = valorPercentual;
            IdSeguroProposta = idSeguroProposta;
            IdSeguroClienteIcatu = idSeguroClienteIcatu;
            IdSeguroParentescoIcatu = idSeguroParentescoIcatu;
        }

        public void SetSeguroProposta(int id) => IdSeguroProposta = id;
    }
}
