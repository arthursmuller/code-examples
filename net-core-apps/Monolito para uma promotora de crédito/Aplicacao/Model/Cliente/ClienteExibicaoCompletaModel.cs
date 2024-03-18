using Aplicacao.Model.DocumentoIdentificacaoCliente;
using Aplicacao.Model.EnderecoCliente;
using Aplicacao.Model.EstadoCivil;
using Aplicacao.Model.Genero;
using Aplicacao.Model.GrauInstrucao;
using Aplicacao.Model.Municipio;
using Aplicacao.Model.RendimentoCliente;
using Aplicacao.Model.TelefoneCliente;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Model.Cliente
{
    public class ClienteExibicaoCompletaModel
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

        public IEnumerable<TelefoneClienteExibicaoModel> Telefones { get; set; }

        public IEnumerable<DocumentoIdentificacaoClienteExibicaoModel> DocumentosIdentificacao { get; set; }

        public IEnumerable<EnderecoClienteExibicaoModel> Enderecos { get; set; }

        public IEnumerable<RendimentoClienteExibicaoModel> Rendimentos { get; set; }

        public ClienteExibicaoCompletaModel(ClienteDominio cliente)
        {
            Id = cliente.ID;
            Nome = cliente.Nome;
            Cpf = cliente.Usuario.CPF;
            DataNascimento = cliente.DataNascimento;
            Filiacao1 = cliente.Filiacao1;
            Filiacao2 = cliente.Filiacao2;
            DeficienteVisual = cliente.DeficienteVisual;
            ImportacaoDadosAutorizada = cliente.ImportacaoDadosAutorizada ?? false;
            DataAutorizacaoImportacaoDados = cliente.DataAutorizacaoImportacaoDados;
            ImportacaoDadosSolicitada = cliente.ImportacaoDadosSolicitada ?? false;
            DataSolicitacaoImportacaoDados = cliente.DataSolicitacaoImportacaoDados;
            Genero = cliente.Genero == null ? null : new GeneroModel { Id = cliente.Genero.ID, Descricao = cliente.Genero.Descricao, Sigla = cliente.Genero.Sigla };
            EstadoCivil = cliente.EstadoCivil == null ? null : new EstadoCivilModel { Id = cliente.EstadoCivil.ID, Descricao = cliente.EstadoCivil.Descricao, Sigla = cliente.EstadoCivil.Sigla };
            GrauInstrucao = cliente.GrauInstrucao == null ? null : new GrauInstrucaoModel { Id = cliente.GrauInstrucao.ID, Descricao = cliente.GrauInstrucao.Descricao };
            CidadeNatal = cliente.CidadeNatal == null ? null : new MunicipioModel(cliente.CidadeNatal);

            Telefones = new List<TelefoneClienteExibicaoModel>()
            {
                new TelefoneClienteExibicaoModel(cliente.TelefonePrincipal),
                new TelefoneClienteExibicaoModel(cliente.TelefoneSecundario),
            };
            
            DocumentosIdentificacao = cliente.DocumentosIdentificacao
                .Where(d => !d.Deletado)
                .OrderBy(d => d.ID)
                .Select(documento => new DocumentoIdentificacaoClienteExibicaoModel(documento))
                .ToList();
            Enderecos = cliente.Enderecos
                .Where(e => !e.Deletado)
                .OrderBy(e => e.ID)
                .Select(endereco => new EnderecoClienteExibicaoModel(endereco))
                .ToList();
            Rendimentos = cliente.Rendimentos
                .Where(d => !d.Deletado)
                .OrderBy(d => d.ID)
                .Select(rendimento => new RendimentoClienteExibicaoModel(rendimento))
                .ToList();
        }
    }
}