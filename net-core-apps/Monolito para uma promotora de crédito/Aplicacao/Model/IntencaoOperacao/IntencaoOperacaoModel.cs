using Aplicacao.Model.Produto;
using Aplicacao.Model.RendimentoCliente;
using Aplicacao.Model.TipoOperacao;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.IntencaoOperacao
{
    [ExcludeFromCodeCoverage]
    public class IntencaoOperacaoModel
    {
        public int Id { get; set; }

        public decimal Prestacao { get; set; }

        public decimal ValorAuxilioFinanceiro { get; set; }

        public decimal TaxaMes { get; set; }

        public decimal TaxaAno { get; set; }

        public decimal ValorFinanciado { get; set; }

        public int Prazo { get; set; }

        public string Proposta { get; set; }

        public bool PermiteAtualizacoes { get; set; }

        public IEnumerable<IntencaoOperacaoObservacaoModel> Observacoes { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public DateTime PrimeiroVencimento { get; set; }

        public TipoOperacaoModel TipoOperacao { get; set; }

        public LojaModel Loja { get; set; }

        public LeadModel Lead { get; set; }

        public UsuarioModel Usuario { get; set; }

        public ProdutoModel Produto { get; set; }

        public RendimentoClienteExibicaoModel Rendimento { get; set; }

        public IEnumerable<IntencaoOperacaoPassoModel> PassosProduto { get; set; }

        public IntencaoOperacaoPortabilidadeVisualizacaoModel Portabilidade { get; set; }

        public IEnumerable<IntencaoOperacaoContratoModel> Contratos { get; set; }

        public IEnumerable<ProximoPassoModel> OpcoesProximoPasso { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class IntencaoOperacaoObservacaoModel
    {
        public string Observacao { get; set; }
        public string DataInclusao { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ProximoPassoModel
    {
        public int IdIntencaoOperacaoSituacao { get; set; }
        public string Titulo { get; set; }
        public bool Excecao { get; set; }
        public bool Excepcional { get; set; }
    }
}
