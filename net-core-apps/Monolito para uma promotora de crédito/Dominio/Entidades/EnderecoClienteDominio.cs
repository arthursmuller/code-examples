using System;

namespace Dominio
{
    public class EnderecoClienteDominio : EnderecoBaseDominio
    {
        public int IdCliente { get; private set; }

        public string Titulo { get; private set; }

        public bool Principal { get; private set; }
        public bool Deletado { get; private set; }

        public ClienteDominio Cliente { get; private set; }

        public EnderecoClienteDominio(int idCliente, string titulo, int idMunicipio, string bairro, int idTipoLogradouro, string logradouro, int? numero, string complemento, string cep, bool principal) 
            : base(idMunicipio, bairro, idTipoLogradouro, logradouro, numero, complemento, cep)
        {
            IdCliente = idCliente;
            Titulo = titulo;
            Principal = principal;
            Deletado = false;
        }

        public EnderecoClienteDominio( string titulo, int idMunicipio, string bairro, int idTipoLogradouro, string logradouro, int? numero, string complemento, string cep, bool principal)
            : base(idMunicipio, bairro, idTipoLogradouro, logradouro, numero, complemento, cep)
        {
            Titulo = titulo;
            Principal = principal;
            Deletado = false;
        }

        public void SetAtualizarEndereco(string titulo, int idMunicipio, string bairro, int idTipoLogradouro, string logradouro, int? numero, string complemento, string cep, bool principal)
        {
            base.SetAtualizarEndereco(idMunicipio, bairro, idTipoLogradouro, logradouro, numero, complemento, cep);

            Titulo = titulo;
            Principal = principal;
            Deletado = false;
        }

        public void AlternarAtivo(bool ativo)
        {
            Deletado = !ativo;
            DataAtualizacao = DateTime.Now;
            atualizarData();
        }

        public void SetPrincipal(bool principal)
        {
            Principal = principal;
            atualizarData();
        }

        public void AlternarDeletado(bool deletado)
        {
            Deletado = deletado;
            atualizarData();
        }

        private void atualizarData()
        {
            DataAtualizacao = DateTime.Now;
        } 

    }
}
