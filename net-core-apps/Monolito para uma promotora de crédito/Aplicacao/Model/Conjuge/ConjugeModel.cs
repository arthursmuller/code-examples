using Aplicacao.Model.Genero;
using Aplicacao.Model.TipoRegimeCasamento;
using System;

namespace Aplicacao.Model.Conjuge
{
    public class ConjugeModel
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? IdCliente { get; set; }
        public int? IdGenero { get; set; }
        public int? IdTipoRegimeCasamento { get; set; }

        public ConjugeModel(string cpf, string nome, DateTime? dataNascimento, int? idCliente, int? idGenero, int? idTipoRegimeCasamento)
        {
            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            IdCliente = idCliente;
            IdGenero = idGenero;
            IdTipoRegimeCasamento = idTipoRegimeCasamento;
        }
    }
}
