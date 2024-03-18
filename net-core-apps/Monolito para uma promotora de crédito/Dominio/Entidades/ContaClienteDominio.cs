using System.Collections.Generic;
using Dominio.Enum;

namespace Dominio
{
    public class ContaClienteDominio : EntidadeBase
    {
        public int IdCliente { get; private set; }
        public int IdBanco { get; private set; }
        public TipoConta IdTipoConta { get; private set; }
        public string Agencia { get; private set; }
        public string Conta { get; private set; }
        public bool Deletado { get; private set; } = false;
        public FormaRecebimento? IdFormaRecebimento { get; private set; }

        public ClienteDominio Cliente { get; private set; }
        public IEnumerable<RendimentoClienteDominio> Rendimentos { get; private set; }
        public IEnumerable<RendimentoClienteDominio> RendimentosRecebimento { get; private set; }
        public BancoDominio Banco { get; private set; }
        public TipoContaDominio TipoConta { get; private set; }
        public FormaRecebimentoDominio FormaRecebimento { get; private set; }
            
        public ContaClienteDominio() { }

        public ContaClienteDominio(int idCliente, int idBanco, TipoConta idTipoConta, string agencia, string conta, FormaRecebimento? idFormaRecebimento)
        {
            IdCliente = idCliente;
            IdBanco = idBanco;
            IdTipoConta = idTipoConta;
            Agencia = agencia;
            Conta = conta;
            IdFormaRecebimento = idFormaRecebimento;
        }

        public void SetPropriedadesAtualizadas(int idBanco, TipoConta idTipoConta, string agencia, string conta)
        {
            IdBanco = idBanco;
            IdTipoConta = idTipoConta;
            Agencia = agencia;
            Conta = conta;

            setDataAtualizacao();
        }

        public void SetDeletado()
        {
            Deletado = true;
            setDataAtualizacao();
        }
    }
}
