using System;

namespace Dominio
{
    public class EnderecoBaseDominio : EntidadeBase
    {
        public int IdMunicipio { get; private set; }
        public string Bairro { get; private set; }
        public int IdTipoLogradouro { get; private set; }
        public string Logradouro { get; private set; }
        public int? Numero { get; private set; }
        public string Complemento { get; private set; }

        private string _cep;
        public string Cep { get => _cep; private set => _cep = desmascararCep(value); }

        public MunicipioDominio Municipio { get; private set; }
        public TipoLogradouroDominio TipoLogradouro { get; private set; }

        public EnderecoBaseDominio() {}

        public EnderecoBaseDominio(int idMunicipio, string bairro, int idTipoLogradouro, string logradouro, int? numero, string complemento, string cep)
        {
            IdMunicipio = idMunicipio;
            Bairro = bairro;
            IdTipoLogradouro = idTipoLogradouro;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
        }

        public void SetAtualizarEndereco(int idMunicipio, string bairro, int idTipoLogradouro, string logradouro, int? numero, string complemento, string cep)
        {
            IdMunicipio = idMunicipio;
            Bairro = bairro;
            IdTipoLogradouro = idTipoLogradouro;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;

            DataAtualizacao = DateTime.Now;
        }

        private string desmascararCep(string cep)
        {
            if (!string.IsNullOrWhiteSpace(cep))
                return cep.Trim().Replace("-", "");

            return cep;
        }
    }
}
