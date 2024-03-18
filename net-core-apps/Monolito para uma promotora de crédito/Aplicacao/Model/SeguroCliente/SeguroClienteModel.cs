using System;

namespace Aplicacao.Model.SeguroCliente
{
    public class SeguroClienteModel
    {
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public char Nacionalidade { get; set; }
        public char PPE { get; set; }
        public decimal RendaMensal { get; set; }
        public bool ResidentePais { get; set; }
        public bool RelacionamentoEletronico { get; set; }
        public bool Aposentado { get; set; }
        public int IdCliente { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdGenero { get; set; }
        public int? IdProfissaoIcatu { get; set; }
    }
}
