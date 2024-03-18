using Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class IntencaoOperacaoDominio : EntidadeBase
    {
        public TipoOperacao IdTipoOperacao { get; private set; }
        public Produto IdProduto { get; private set; }
        public int? IdLoja { get; private set; }
        public int? IdLead { get; private set; }
        public int? IdUsuario { get; private set; }
        public int? IdRendimentoCliente { get; private set; }
        public decimal Prestacao { get; private set; }
        public decimal ValorAuxilioFinanceiro { get; private set; }
        public decimal TaxaMes { get; private set; }
        public decimal TaxaAno { get; private set; }
        public decimal ValorFinanciado { get; private set; }
        public int Prazo { get; private set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime PrimeiroVencimento { get; set; } = DateTime.Now;
        public decimal ImpostoOperacaoFinanceira { get; private set; }
        public decimal CustoEfetivoTotalMes { get; private set; }
        public decimal CustoEfetivoTotalAno { get; private set; }
        public string Proposta { get; private set; }

        public RendimentoClienteDominio RendimentoCliente { get; private set; }
        public UsuarioDominio Usuario { get; private set; }
        public LeadDominio Lead { get; private set; }
        public LojaDominio Loja { get; private set; }
        public TipoOperacaoDominio TipoOperacao { get; private set; }
        public ProdutoDominio Produto { get; private set; }
        public IEnumerable<IntencaoOperacaoHistoricoDominio> Historico { get; private set; }
        public IntencaoOperacaoPortabilidadeDominio Portabilidade { get; private set; }
        public IEnumerable<IntencaoOperacaoContratoDominio> Contratos { get; private set; }
        public IEnumerable<IntencaoOperacaoObservacaoDominio> Observacoes { get; private set; }

        public IntencaoOperacaoSituacaoDominio Situacao { get => Historico?.OrderBy(h => h.DataAtualizacao).Last().SituacaoIntencaoOperacao; }

        protected IntencaoOperacaoDominio() { }

        public IntencaoOperacaoDominio(TipoOperacao idTipoOperacao, Produto idProduto, int? idLoja, int? idLead, int? idUsuario, decimal prestacao, decimal valorAuxilioFinanceiro,
            decimal taxaMes, decimal taxaAno, decimal valorFinanciado, int prazo, DateTime primeiroVencimento, int? idRendimentoCliente, decimal impostoOperacaoFinanceira
            , decimal custoEfetivoTotalMes, decimal custoEfetivoTotalAno, IEnumerable<IntencaoOperacaoContratoDominio> contratos)
        {
            IdTipoOperacao = idTipoOperacao;
            IdProduto = idProduto;
            Prestacao = prestacao;
            Prazo = prazo;
            ValorFinanciado = valorFinanciado;
            ValorAuxilioFinanceiro = valorAuxilioFinanceiro;
            TaxaMes = taxaMes;
            TaxaAno = taxaAno;
            ImpostoOperacaoFinanceira = impostoOperacaoFinanceira;
            CustoEfetivoTotalMes = custoEfetivoTotalMes;
            CustoEfetivoTotalAno = custoEfetivoTotalAno;
            PrimeiroVencimento = primeiroVencimento;

            IdRendimentoCliente = idRendimentoCliente;
            IdUsuario = idUsuario;
            IdLoja = idLoja;
            IdLead = idLead;

            Contratos = contratos;
        }

        public void SetPropriedadesAtualizadas(TipoOperacao idTipoOperacao, int? idLoja, decimal prestacao, decimal valorAuxilioFinanceiro, decimal taxaMes, decimal taxaAno, decimal valorFinanciado,
            int prazo, DateTime primeiroVencimento, int? idRendimentoCliente, decimal impostoOperacaoFinanceira, decimal custoEfetivoTotalMes, decimal custoEfetivoTotalAno)
        {
            IdTipoOperacao = idTipoOperacao;
            Prestacao = prestacao;
            Prazo = prazo;
            ValorFinanciado = valorFinanciado;
            ValorAuxilioFinanceiro = valorAuxilioFinanceiro;
            TaxaMes = taxaMes;
            TaxaAno = taxaAno;
            ImpostoOperacaoFinanceira = impostoOperacaoFinanceira;
            CustoEfetivoTotalMes = custoEfetivoTotalMes;
            CustoEfetivoTotalAno = custoEfetivoTotalAno;
            PrimeiroVencimento = primeiroVencimento;

            IdRendimentoCliente = idRendimentoCliente;
            IdLoja = idLoja;

            setDataAtualizacao();
        }

        public void SetPortabilidade(IntencaoOperacaoPortabilidadeDominio portabilidade)
        {
            Portabilidade = portabilidade;

            setDataAtualizacao();
        }

        public void SetProposta(string proposta)
        {
            Proposta = proposta;

            setDataAtualizacao();
        }

        public void SetAtualizacaoPortabilidade(IntencaoOperacaoPortabilidadeDominio portabilidade)
        {
            Portabilidade.SetPropriedadesAtualizadas(portabilidade.IdBancoOriginador, portabilidade.PrazoRestante, portabilidade.PrazoTotal, portabilidade.SaldoDevedor,
                portabilidade.PlanoRefinanciamento, portabilidade.PrazoRefinanciamento, portabilidade.ValorPrestacaoRefinanciamento);

            setDataAtualizacao();
        }


        public void SetAtendimentoIntencaoOperacao(int? idLoja, string proposta = null)
        {
            setDataAtualizacao();
            if (idLoja.HasValue)
                IdLoja = idLoja;
            
            if (!String.IsNullOrWhiteSpace(proposta))
                Proposta = proposta;
        }

        public IntencaoOperacaoObservacaoDominio AdicionarObservacao(string observacao)
        {
            setDataAtualizacao();
            return new IntencaoOperacaoObservacaoDominio(ID, observacao);
        }

        public IntencaoOperacaoHistoricoDominio SetSituacaoIntencaoOperacao(int idSituacao, string descricaoEspecifica = null, bool pendenciaUsuario = false)
        {
            setDataAtualizacao();
            var situacao = new IntencaoOperacaoHistoricoDominio(ID, idSituacao, descricaoEspecifica, pendenciaUsuario);
            return situacao;
        }
    }
}
