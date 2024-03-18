using Aplicacao.Model.Conjuge;
using Aplicacao.Model.EstadoCivil;
using Aplicacao.Model.Genero;
using Aplicacao.Model.GrauInstrucao;
using Aplicacao.Model.Municipio;
using Dominio;
using System;

namespace Aplicacao.Model.Cliente
{
    public class ClienteExibicaoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Filiacao1 { get; set; }

        public string Filiacao2 { get; set; }

        public bool? DeficienteVisual { get; set; }

        public bool ImportacaoDadosAutorizada { get; set; }

        public DateTime? DataAutorizacaoImportacaoDados { get; set; }
        
        public bool ImportacaoDadosSolicitada { get; set; }

        public DateTime? DataSolicitacaoImportacaoDados { get; set; }

        public GeneroModel Genero { get; set; }

        public EstadoCivilModel EstadoCivil { get; set; }

        public GrauInstrucaoModel GrauInstrucao { get; set; }

        public MunicipioModel CidadeNatal { get; set; }
        public ConjugeModel Conjuge { get; set; }

        public LojaModel Loja { get; set; }
        public bool? OperacaoAtiva { get; set; }

        public ClienteExibicaoModel() { }

        public ClienteExibicaoModel(ClienteDominio cliente)
        {
            Id = cliente.ID;
            Nome = cliente.Nome;
            Cpf = cliente.Usuario.CPF;
            DataNascimento = cliente.DataNascimento;
            Filiacao1 = cliente.Filiacao1;
            Filiacao2 = cliente.Filiacao2;
            DeficienteVisual = cliente.DeficienteVisual;
            Genero = cliente.Genero == null ? null : new GeneroModel { Id = cliente.Genero.ID, Descricao = cliente.Genero.Descricao, Sigla = cliente.Genero.Sigla };
            EstadoCivil = cliente.EstadoCivil == null ? null : new EstadoCivilModel { Id = cliente.EstadoCivil.ID, Descricao = cliente.EstadoCivil.Descricao, Sigla = cliente.EstadoCivil.Sigla };
            GrauInstrucao = cliente.GrauInstrucao == null ? null : new GrauInstrucaoModel { Id = cliente.GrauInstrucao.ID, Descricao = cliente.GrauInstrucao.Descricao };
            CidadeNatal = cliente.CidadeNatal == null ? null : new MunicipioModel(cliente.CidadeNatal);
            Loja = cliente.Loja == null ? null : new LojaModel(cliente.Loja);
            ImportacaoDadosAutorizada = cliente.ImportacaoDadosAutorizada ?? false;
            DataAutorizacaoImportacaoDados = cliente.DataAutorizacaoImportacaoDados;
            ImportacaoDadosSolicitada = cliente.ImportacaoDadosSolicitada ?? false;
            DataSolicitacaoImportacaoDados = cliente.DataSolicitacaoImportacaoDados;
            Conjuge = cliente.Conjuge == null ? null : new ConjugeModel(cliente.Conjuge.CPF, cliente.Conjuge.Nome, cliente.Conjuge.DataNascimento, cliente.Conjuge.IdCliente, cliente.Conjuge.IdGenero, cliente.Conjuge.IdTipoRegimeCasamento);
            OperacaoAtiva = cliente.OperacaoAtiva;
        }
    }
}
