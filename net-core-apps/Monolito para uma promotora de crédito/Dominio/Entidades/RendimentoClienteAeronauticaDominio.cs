using Dominio.Enum;
using System;

namespace Dominio
{
    public class RendimentoClienteAeronauticaDominio : RendimentoClienteDominio
    {
        public int IdAeronauticaTipoFuncional { get; private set; }
        public int? IdAeronauticaCargo { get; private set; }

        public AeronauticaTipoFuncionalDominio TipoFuncional { get; set; }
        public AeronauticaCargoDominio Cargo { get; set; }

        public RendimentoClienteAeronauticaDominio(int idContaCliente, int idContaClienteRecebimento, int idCliente, Convenio idConvenio, int idConvenioOrgao, int idUf, decimal valorRendimento, string matricula,
            int idAeronauticaTipoFuncional, int? idAeronauticaCargo)
                : base(idContaCliente, idContaClienteRecebimento, idCliente, idConvenio, idConvenioOrgao, idUf, valorRendimento, matricula)
        {
            IdAeronauticaTipoFuncional = idAeronauticaTipoFuncional;
            IdAeronauticaCargo = idAeronauticaCargo;
        }

        public void SetPropriedadesAtualizadas(int idUf, decimal valorRendimento, string matricula, int? idConvenioOrgao,
            int idAeronauticaTipoFuncional, int? idAeronauticaCargo, decimal? margemDisponivel, decimal? margemDisponivelCartao)
        {
            IdAeronauticaTipoFuncional = idAeronauticaTipoFuncional;
            IdAeronauticaCargo = idAeronauticaCargo;
            DataAtualizacao = DateTime.Now;

            base.SetPropriedadesAtualizadas(idUf, valorRendimento, matricula, idConvenioOrgao, margemDisponivel, margemDisponivelCartao);
        }
    }
}
