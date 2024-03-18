using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]
    public class Cpfcnpj
    {
        public string Numero { get; set; }
        public readonly string Origem = "T - Titular";
    }

    public class Documento
    {
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public string OrgaoEmissor { get; set; }
    }

    public class Telefone
    {
        public string Ddd { get; set; }
        public string Numero { get; set; }
    }

    public class Residencial
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public Telefone Telefone { get; set; }
    }

    public class Correspondencia
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public Telefone Telefone { get; set; }
    }

    public class Endereco
    {
        public Residencial Residencial { get; set; }
        public Correspondencia Correspondencia { get; set; }
    }

    public class ResidenciaFiscal
    {
        public string Pais { get; set; }
        public string MotivoSemNIF { get; set; }
    }

    public class DadosCadastrais
    {
        public readonly string TipoPessoa = "F - Física";
        public readonly string Nacionalidade = "B - Brasileiro, E - Estrangeiro";
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }
        public int IdOcupacao { get; set; }
        public decimal RendaMensal { get; set; }
        public int IdEstadoCivil { get; set; }
        public string Email { get; set; }
        public bool ResidentePais { get; set; }
        public bool Ppe { get; set; }
        public bool RelacionamentoEletronico { get; set; }
        public bool ComprovanteLegal { get; set; }
        public string Aposentado { get; set; }
        public Documento Documento { get; set; }
        public Endereco Endereco { get; set; }
        public ResidenciaFiscal ResidenciaFiscal { get; set; }
        public Cpfcnpj Cpfcnpj { get; set; }
    }

    public class Agencia { }

    public class ContaCorrente { }

    public class Banco
    {
        public Agencia Agencia { get; set; }
        public ContaCorrente ContaCorrente { get; set; }
    }

    public class Correntista { }

    public class FormaCobranca
    {
        public readonly int Periodicidade = 1;
        public readonly int TipoCobranca  = 7;
        public readonly int TipoDemaisParcelas  = 7;
        public decimal PremioTotal { get; set; }
        public string QuatroUltimosDigitosCartao { get; set; }
        public bool PrimeiroPremioPago { get; set; } = true;
        public string IdCartao { get; set; }
        public string IdCobrancaCartao { get; set; }
        public Banco Banco { get; set; }
        public Correntista Correntista { get; set; }
    }

    public class Cobertura
    {
        public int Modulo { get; set; }
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Capital { get; set; }
        public decimal Premio { get; set; }
        public string TipoProponente { get; set; }
    }

    public class CriarPropostaDto
    {
        public readonly bool RestricaoDps = false;
        public long Id { get; set; }
        public int Modulo { get; set; } = 1;
        public int GrupoApolice { get; set; } = 57162;
        public int Subestipulante { get; set; } = 0;
        public string IdPdv { get; set; } = "99999";
        public int IdParceria { get; set; }  = 0428;
        public string DataAssinatura { get; private set; } = String.Format("{0:u}", DateTime.UtcNow);
        public DadosCadastrais DadosCadastrais { get; set; }
        public FormaCobranca FormaCobranca { get; set; }
        public List<Cobertura> Coberturas { get; set; }
    }
}
