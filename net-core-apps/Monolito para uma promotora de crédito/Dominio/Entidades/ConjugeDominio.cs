using SharedKernel.ValueObjects.v2;
using System;

namespace Dominio
{
    public class ConjugeDominio : EntidadeBase
    {
        private string _cpf;
        public string CPF { get => _cpf; private set => _cpf = value?.Trim(); }
        public string Nome { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public int? IdCliente { get; private set; }
        public ClienteDominio Cliente { get; private set; }
        public int? IdGenero { get; private set; }
        public GeneroDominio Genero { get; private set; }
        public int? IdTipoRegimeCasamento { get; private set; }
        public TipoRegimeCasamentoDominio TipoRegimeCasamento { get; private set; }
        public ConjugeDominio() { }
        public ConjugeDominio(CPF cpf, string nome, DateTime? dataNascimento, int? idCliente, int? idGenero, int? idTipoRegimeCasamento)
        {
            CPF = cpf.ToString();
            Nome = nome;
            DataNascimento = dataNascimento;
            IdCliente = idCliente;
            IdGenero = idGenero;
            IdTipoRegimeCasamento = idTipoRegimeCasamento;
        }

        public void SetGenero(int? idGenero)
        {
            IdGenero = idGenero ?? IdGenero;
            setDataAtualizacao();
        }

        public void SetTipoRegimeCasamento(int? idTipoRegimeCasamento)
        {
            IdTipoRegimeCasamento = idTipoRegimeCasamento ?? IdTipoRegimeCasamento;
            setDataAtualizacao();
        }

        public void SetPropriedadesAtualizadas(CPF cpf, string nome, DateTime? dataNascimento)
        {
            CPF = cpf.ToString() ?? CPF;
            Nome = nome ?? Nome;
            DataNascimento = dataNascimento ?? dataNascimento;
            setDataAtualizacao();
        }
    }
}
