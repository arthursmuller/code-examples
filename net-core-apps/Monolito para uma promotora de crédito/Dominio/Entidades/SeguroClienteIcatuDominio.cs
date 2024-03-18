using System;
using System.Collections.Generic;

namespace Dominio
{
    public class SeguroClienteIcatuDominio : EntidadeBase
    {
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public char Nacionalidade { get; set; }
        public char PPE { get; set; }
        public decimal RendaMensal { get; set; }
        public bool ResidentePais { get; set; }
        public bool RelacionamentoEletronico { get; set; }
        public bool Aposentado { get; set; }
        public int? IdCliente { get; private set; }
        public ClienteDominio Cliente { get; private set; }
        public int? IdEstadoCivil { get; private set; }
        public EstadoCivilDominio EstadoCivil { get; private set; }
        public int? IdGenero { get; private set; }
        public GeneroDominio Genero { get; private set; }
        public int? IdProfissaoIcatu { get; private set; }
        public SeguroProfissaoIcatuDominio SeguroProfissaoIcatu { get; private set; }
        public ICollection<SeguroClienteTelefoneDominio> Telefones { get; private set; }

        public int? IdEnderecoPrincipal { get; private set; }
        public int? IdEnderecoCobranca { get; private set; }
        public SeguroEnderecoClienteDominio EnderecoPrincipal { get; private set; }
        public SeguroEnderecoClienteDominio EnderecoCobranca { get; private set; }

        public SeguroClienteIcatuDominio() { }

        public SeguroClienteIcatuDominio(DateTime? dataNascimento, string email, string? nome, char nacionalidade, char pPE, decimal rendaMensal, bool residentePais, bool relacionamentoEletronico, bool aposentado, int? idCliente, int? idEstadoCivil, int? idGenero, int? idProfissaoIcatu)
        {
            DataNascimento = dataNascimento;
            Email = email;
            Nome = nome;
            Nacionalidade = nacionalidade;
            PPE = pPE;
            RendaMensal = rendaMensal;
            ResidentePais = residentePais;
            RelacionamentoEletronico = relacionamentoEletronico;
            Aposentado = aposentado;
            IdCliente = idCliente;
            IdEstadoCivil = idEstadoCivil;
            IdGenero = idGenero;
            IdProfissaoIcatu = idProfissaoIcatu;
        }

        public void SetTelefone(SeguroClienteTelefoneDominio telefone) => getTelefones().Add(telefone);
        public ICollection<SeguroClienteTelefoneDominio> getTelefones()
            => this.Telefones ?? Array.Empty<SeguroClienteTelefoneDominio>();

        public void SetEnderecoPrincipal(SeguroEnderecoClienteDominio novoEndereco) => EnderecoPrincipal = novoEndereco;
        public void SetEnderecoCobranca(SeguroEnderecoClienteDominio novoEndereco) => EnderecoCobranca = novoEndereco;
    }
}
