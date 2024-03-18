using System;
using System.Collections.Generic;

namespace Dominio
{
    public class ClienteDominio : EntidadeBase
    {
        public int? IdGenero { get; private set; }
        public int? IdConjuge { get; private set; }
        public int? IdEstadoCivil { get; private set; }
        public int? IdGrauInstrucao { get; private set; }
        public int? IdCidadeNatal { get; private set; }
        public string Nome { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string Filiacao1 { get; private set; }
        public string Filiacao2 { get; private set; }
        public bool? DeficienteVisual { get; private set; }
        public int IdUsuario { get; private set; }
        public DateTime? DataImportacao { get; private set; }
        public int? IdLoja { get; private set; }
        public bool? ImportacaoDadosAutorizada { get; private set; }
        public int? IdProfissao { get; private set; }
        public DateTime? DataAutorizacaoImportacaoDados { get; private set; }
        public bool? ImportacaoDadosSolicitada { get; private set; }
        public DateTime? DataSolicitacaoImportacaoDados { get; private set; }

        public int? IdTelefonePrincipal { get; private set; }
        public int? IdTelefoneSecundario { get; private set; }

        public int? IdEnderecoPrincipal { get; private set; }
        public int? IdEnderecoSecundario { get; private set; }
        public int? IdContaBancaria { get; private set; }
        public bool? OperacaoAtiva { get; set; }
        public ContaBancariaDominio ContaBancaria { get; private set; }
        public LojaDominio Loja { get; private set; }
        public UsuarioDominio Usuario { get; private set; }
        public GeneroDominio Genero { get; private set; }
        public ConjugeDominio Conjuge { get; private set; }
        public EstadoCivilDominio EstadoCivil { get; private set; }
        public GrauInstrucaoDominio GrauInstrucao { get; private set; }
        public MunicipioDominio CidadeNatal { get; private set; }
        public TelefoneClienteDominio TelefonePrincipal { get; private set; }
        public TelefoneClienteDominio TelefoneSecundario { get; private set; }
        public EnderecoClienteDominio EnderecoPrincipal { get; private set; }
        public EnderecoClienteDominio EnderecoSecundario { get; private set; }
        public SeguroProfissaoDominio Profissao { get; private set; }
        public SeguroPropostaDominio SeguroProposta { get; private set; }
        public BiometriaClienteDominio BiometriaCliente { get; private set; }

        public IEnumerable<TelefoneClienteDominio> Telefones { get; private set; }
        public IEnumerable<DocumentoIdentificacaoClienteDominio> DocumentosIdentificacao { get; private set; }
        public IEnumerable<EnderecoClienteDominio> Enderecos { get; private set; }
        public IEnumerable<RendimentoClienteDominio> Rendimentos { get; private set; }
        public IEnumerable<AnexoDominio> Anexos { get; private set; }
        public IEnumerable<ContaClienteDominio> Contas { get; private set; }


        public ClienteDominio()
        {
            Telefones = new List<TelefoneClienteDominio>();
            DocumentosIdentificacao = new List<DocumentoIdentificacaoClienteDominio>();
            Enderecos = new List<EnderecoClienteDominio>();
            Rendimentos = new List<RendimentoClienteDominio>();
        }

        public ClienteDominio(string nome)
            => Nome = nome;

        public ClienteDominio(int idGenero, int idEstadoCivil, int idGrauInstrucao, int idCidadeNatal, string nome, DateTime dataNascimento, string filiacao1, string filiacao2, bool deficienteVisual, int idUsuario, int? idTelefonePrincipal, int? idTelefoneSecundario)
        {
            IdGenero = idGenero;
            IdEstadoCivil = idEstadoCivil;
            IdGrauInstrucao = idGrauInstrucao;
            IdCidadeNatal = idCidadeNatal;
            Nome = nome;
            DataNascimento = dataNascimento;
            Filiacao1 = filiacao1;
            Filiacao2 = filiacao2;
            DeficienteVisual = deficienteVisual;
            IdUsuario = idUsuario;
            IdTelefonePrincipal = idTelefonePrincipal;
            IdTelefoneSecundario = idTelefoneSecundario;
        }

        public void SetPropriedadesAtualizadas(int idGenero, int idEstadoCivil, int idGrauInstrucao, int idCidadeNatal, string nome, DateTime dataNascimento, string filiacao1, string filiacao2, bool deficienteVisual, DateTime? dataImportacao, int? idLoja, int? idProfissao)
        {
            IdGenero = idGenero;
            IdEstadoCivil = idEstadoCivil;
            IdGrauInstrucao = idGrauInstrucao;
            IdCidadeNatal = idCidadeNatal;
            Nome = nome;
            DataNascimento = dataNascimento;
            Filiacao1 = filiacao1;
            Filiacao2 = filiacao2;
            DeficienteVisual = deficienteVisual;
            DataImportacao = dataImportacao;
            IdLoja = idLoja;
            IdProfissao = idProfissao;

            setDataAtualizacao();
        }

        public void SetTelefonePrincipal(int? idTelefonePrincipal)
        {
            IdTelefonePrincipal = idTelefonePrincipal;
            setDataAtualizacao();
        }

        public void SetTelefoneSecundario(int? idTelefoneSecundario)
        {
            IdTelefoneSecundario = idTelefoneSecundario;
            setDataAtualizacao();
        }

        public void SetContaBancaria(int? idContaBancaria)
        {
            IdContaBancaria = idContaBancaria;
            setDataAtualizacao();
        }

        public void SetEnderecoPrincipal(int? idEnderecoPrincipal)
        {
            IdEnderecoPrincipal = idEnderecoPrincipal;
            setDataAtualizacao();
        }

        public void SetEnderecoSecundario(int? idEnderecoSecundario)
        {
            IdEnderecoSecundario = idEnderecoSecundario;
            setDataAtualizacao();
        }
        public void SetProfissao(int? idProfissao)
        {
            IdProfissao = idProfissao;
            setDataAtualizacao();
        }
        public void SetSolicitarImportacaoDados()
        {
            ImportacaoDadosSolicitada = true;
            DataSolicitacaoImportacaoDados = DateTime.Now;

            setDataAtualizacao();
        }

        public void SetAutorizacaoImportacaoDados(bool autorizacaoConcedida)
        {
            ImportacaoDadosAutorizada = autorizacaoConcedida;
            DataAutorizacaoImportacaoDados = DateTime.Now;

            setDataAtualizacao();
        }

        public void SetConjuge(int? idConjuge)
        {
            IdConjuge = idConjuge;
            setDataAtualizacao();
        }

        public void SetOperacaoAtiva(bool operacaoAtiva)
        {
            OperacaoAtiva = operacaoAtiva;
            setDataAtualizacao();
        }
    }
}
