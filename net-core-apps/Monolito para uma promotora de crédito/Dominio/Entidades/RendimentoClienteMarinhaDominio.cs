using Dominio.Enum;
using System;

namespace Dominio
{
    public class RendimentoClienteMarinhaDominio : RendimentoClienteDominio
    {
        public int IdMarinhaTipoFuncional { get; private set; }
        public int? IdMarinhaCargo { get; private set; }

        public MarinhaTipoFuncionalDominio TipoFuncional { get; set; }
        public MarinhaCargoDominio Cargo { get; set; }

        public RendimentoClienteMarinhaDominio(int idContaCliente, int idContaClienteRecebimento, int idCliente, Convenio idConvenio, int idConvenioOrgao, int idUf, decimal valorRendimento, string matricula,
            int idMarinhaTipoFuncional, int? idMarinhaCargo)
                : base(idContaCliente, idContaClienteRecebimento, idCliente, idConvenio, idConvenioOrgao, idUf, valorRendimento, matricula)
        {
            IdMarinhaTipoFuncional = idMarinhaTipoFuncional;
            IdMarinhaCargo = idMarinhaCargo;
        }

        public void SetPropriedadesAtualizadas(int idUf, decimal valorRendimento, string matricula, int? idConvenioOrgao,
            int idMarinhaTipoFuncional, int? idMarinhaCargo, decimal? margemDisponivel, decimal? margemDisponivelCartao)
        {
            IdMarinhaTipoFuncional = idMarinhaTipoFuncional;
            IdMarinhaCargo = idMarinhaCargo;
            DataAtualizacao = DateTime.Now;

            base.SetPropriedadesAtualizadas(idUf, valorRendimento, matricula, idConvenioOrgao, margemDisponivel, margemDisponivelCartao);
        }
    }
}
