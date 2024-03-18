using Dominio.Enum;
using System.Collections.Generic;

namespace Dominio
{
    public abstract class RendimentoClienteDominio : EntidadeBase
    {
        public int IdCliente { get; private set; }
        public Convenio IdConvenio { get; private set; }
        public int IdConvenioOrgao { get; private set; }
        public int IdUf { get; private set; }
        public int IdContaCliente { get; private set; }
        public int IdContaClienteRecebimento { get; private set; }
        public decimal ValorRendimento { get; private set; }
        public string Matricula { get; private set; }
        public decimal? MargemDisponivel { get; private set; }
        public decimal? MargemDisponivelCartao { get; private set; }
        public bool Deletado { get; private set; }

        public ClienteDominio Cliente { get; private set; }
        public ConvenioDominio Convenio { get; private set; }
        public ConvenioOrgaoDominio ConvenioOrgao { get; private set; }
        public UnidadeFederativaDominio Uf { get; private set; }
        public IEnumerable<IntencaoOperacaoDominio> IntencoesOperacao { get; private set; }
        public ContaClienteDominio ContaCliente { get; private set; }
        public ContaClienteDominio ContaClienteRecebimento { get; private set; }

        public RendimentoClienteDominio(int idContaCliente, int idContaClienteRecebimento, int idCliente, Convenio idConvenio, int idConvenioOrgao, int idUf, decimal valorRendimento, string matricula)
        {
            IdContaCliente = idContaCliente;
            IdContaClienteRecebimento = idContaClienteRecebimento;
            IdCliente = idCliente;
            IdConvenio = idConvenio;
            IdConvenioOrgao = idConvenioOrgao;
            IdUf = idUf;
            ValorRendimento = valorRendimento;
            Matricula = matricula;
            Deletado = false;
        }

        protected void SetPropriedadesAtualizadas(int idUf, decimal valorRendimento, string matricula, int? idConvenioOrgao, decimal? margemDisponivel, decimal? margemDisponivelCartao)
        {
            IdUf = idUf;
            ValorRendimento = valorRendimento;
            Matricula = matricula;
            MargemDisponivel = margemDisponivel;
            MargemDisponivelCartao = margemDisponivelCartao;
            Deletado = false;

            if (idConvenioOrgao.HasValue)
                IdConvenioOrgao = idConvenioOrgao.Value;

            setDataAtualizacao();
        }

        public void SetContas(int idContaCliente, int idContaClienteRecebimento)
        {
            IdContaClienteRecebimento = idContaClienteRecebimento;
            IdContaCliente = idContaCliente;
        }

        public void AlternarAtivo(bool ativo)
        {
            Deletado = !ativo;
            setDataAtualizacao();
        }
    }
}
