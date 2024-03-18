using Aplicacao.Model.Cliente;
using Aplicacao.Model.Genero;
using Aplicacao.Model.TipoRegimeCasamento;
using System;

namespace Aplicacao.Model.Conjuge
{
    public class ConjugeExibicaoModel
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroModel Genero { get; set; }
        public ClienteExibicaoModel Cliente { get; set; }
        public TipoRegimeCasamentoExibicaoModel TipoRegimeCasamentoModel { get; set; }
        public ConjugeExibicaoModel(
            int id, 
            string cpf, 
            string nome, 
            DateTime? dataNascimento,
            GeneroModel genero, 
            ClienteExibicaoModel cliente,
            TipoRegimeCasamentoExibicaoModel tipoRegimeCasamentoModel)
        {
            ID = id;
            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Genero = genero;
            Cliente = cliente;
            TipoRegimeCasamentoModel = tipoRegimeCasamentoModel;
        }
    }
}
