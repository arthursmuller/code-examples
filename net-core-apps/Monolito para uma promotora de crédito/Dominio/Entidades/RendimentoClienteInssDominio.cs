using Dominio.Enum;
using System;

namespace Dominio
{
    public class RendimentoClienteInssDominio : RendimentoClienteDominio
    {
        public int IdInssEspecieBeneficio { get; private set; }
        public DateTime DataInscricao { get; private set; }

        public InssEspecieBeneficioDominio EspecieBeneficio { get; private set; }

        public RendimentoClienteInssDominio(int idContaCliente, int idContaClienteRecebimento, int idCliente, Convenio idConvenio, int idConvenioOrgao, int idUf, decimal valorRendimento, string matricula,
            int idInssEspecieBeneficio, DateTime dataInscricao)
                : base(idContaCliente, idContaClienteRecebimento, idCliente, idConvenio, idConvenioOrgao, idUf, valorRendimento, matricula)
        {
            IdInssEspecieBeneficio = idInssEspecieBeneficio;
            DataInscricao = dataInscricao;
        }

        public void SetPropriedadesAtualizadas(int idUf, decimal valorRendimento, string matricula, int? idConvenioOrgao,
            int idInssEspecieBeneficio, DateTime dataInscricao, decimal? margemDisponivel, decimal? margemDisponivelCartao)
        {
            IdInssEspecieBeneficio = idInssEspecieBeneficio;
            DataInscricao = dataInscricao;
            DataAtualizacao = DateTime.Now;

            base.SetPropriedadesAtualizadas(idUf, valorRendimento, matricula, idConvenioOrgao, margemDisponivel, margemDisponivelCartao);
        }
    }
}
