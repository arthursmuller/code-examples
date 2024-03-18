using Dominio.Enum;
using System;

namespace Dominio
{
    public class RendimentoClienteSiapeDominio : RendimentoClienteDominio
    {
        public int IdSiapeTipoFuncional { get; private set; }
        public string MatriculaInstituidor { get; private set; }
        public string NomeInstituidor { get; private set; }
        public bool PossuiRepresentacaoPorProcurador { get; private set; }
        public DateTime DataAdmissao { get; private set; }
        public DateTime? DataLiberacaoConsultaMargem { get; private set; }

        public SiapeTipoFuncionalDominio TipoFuncional { get; private set; }

        public RendimentoClienteSiapeDominio(int idContaCliente, int idContaClienteRecebimento, int idCliente, Convenio idConvenio, int idConvenioOrgao, int idUf, decimal valorRendimento, string matricula,
            int idSiapeTipoFuncional, string matriculaInstituidor, bool possuiRepresentacaoPorProcurador, DateTime dataAdmissao, string nomeInstituidor)
                : base(idContaCliente, idContaClienteRecebimento, idCliente, idConvenio, idConvenioOrgao, idUf, valorRendimento, matricula)
        {
            IdSiapeTipoFuncional = idSiapeTipoFuncional;
            MatriculaInstituidor = matriculaInstituidor;
            PossuiRepresentacaoPorProcurador = possuiRepresentacaoPorProcurador;
            DataAdmissao = dataAdmissao;
            NomeInstituidor = nomeInstituidor;
        }

        public void SetPropriedadesAtualizadas(int idUf, decimal valorRendimento, string matricula, int? idConvenioOrgao,
            int idSiapeTipoFuncional, string matriculaInstituidor, bool possuiRepresentacaoPorProcurador, DateTime dataAdmissao, string nomeInstituidor, decimal? margemDisponivel, decimal? margemDisponivelCartao)
        {
            IdSiapeTipoFuncional = idSiapeTipoFuncional;
            MatriculaInstituidor = matriculaInstituidor;
            PossuiRepresentacaoPorProcurador = possuiRepresentacaoPorProcurador;
            DataAdmissao = dataAdmissao;
            NomeInstituidor = nomeInstituidor;
            DataAtualizacao = DateTime.Now;

            base.SetPropriedadesAtualizadas(idUf, valorRendimento, matricula, idConvenioOrgao, margemDisponivel, margemDisponivelCartao);
        }

        public void RegistrarAceiteConsultaMargem()
        {
            DataLiberacaoConsultaMargem = DataAtualizacao = DateTime.Now;
        }
    }
}
