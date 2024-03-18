using Aplicacao.Model.Banco;
using Aplicacao.Model.TipoConta;
using Dominio;

namespace Aplicacao.Model.ContaCliente
{
    public class ContaClienteExibicaoModel
    {
        public int IdContaCliente { get; set; }
        public int IdBanco { get; set; }
        public int IdTipoConta { get; set; }
        public int IdFormaRecebimento { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }

        public BancoModel Banco { get; set; }
        public TipoContaModel TipoConta { get; set; }
        public FormaRecebimentoModel FormaRecebimento { get; set; }

        public ContaClienteExibicaoModel() {}

        public ContaClienteExibicaoModel(ContaClienteDominio conta)
        {
            IdContaCliente = conta.ID;
            IdBanco = conta.IdBanco;
            IdTipoConta = (int)conta.IdTipoConta;
            Agencia = conta.Agencia;
            Conta = conta.Conta;
            IdFormaRecebimento = (int)conta.IdFormaRecebimento;

            Banco = conta.Banco == null
                ? null
                : new BancoModel { Id = conta.Banco.ID, Nome = conta.Banco.Nome, Codigo = conta.Banco.Codigo };
            TipoConta = conta.TipoConta == null 
                ? null
                : new TipoContaModel { Id = (int)conta.TipoConta.ID, Nome = conta.TipoConta.Nome, Sigla = conta.TipoConta.Sigla };
            FormaRecebimento = conta.FormaRecebimento == null
                ? null
                : new FormaRecebimentoModel { Id = (int)conta.IdFormaRecebimento, Descricao = conta.FormaRecebimento.Nome };
        }
    }
}
