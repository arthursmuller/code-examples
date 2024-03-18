using Aplicacao.Model.ContaCliente;
using Aplicacao.Model.Convenio;
using Aplicacao.Model.InssEspecieBeneficio;
using Aplicacao.Model.SiapeTipoFuncional;
using Aplicacao.Model.UnidadeFederativa;
using Dominio;
using System;

namespace Aplicacao.Model.RendimentoCliente
{
    public class RendimentoClienteExibicaoModel
    {
        public int Id { get; set; }
        public ConvenioModel Convenio { get; set; }
        public ConvenioOrgaoModel ConvenioOrgao { get; set; }
        public UnidadeFederativaModel Uf { get; set; }
        public decimal ValorRendimento { get; set; }
        public InssEspecieBeneficioModel InssEspecieBeneficio { get; set; }
        public SiapeTipoFuncionalModel SiapeTipoFuncional { get; set; }
        public string Matricula { get; set; }
        public DateTime DataInscricaoBeneficio { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataLiberacaoConsultaMargem { get; set; }
        public string MatriculaInstituidor { get; set; }
        public string NomeInstituidor { get; set; }
        public bool PossuiRepresentacaoPorProcurador { get; set; }
        public decimal? MargemDisponivel { get; set; }
        public decimal? MargemDisponivelCartao { get; set; }
        public ContaClienteExibicaoModel ContaCliente { get; }
        public ContaClienteExibicaoModel ContaClienteRecebimento { get; }

        public RendimentoClienteExibicaoModel() { }

        public RendimentoClienteExibicaoModel(RendimentoClienteDominio rendimento)
        {
            Id = rendimento.ID;
            Convenio = rendimento.Convenio == null ? null : new ConvenioModel { ID = (int)rendimento.Convenio.ID, Nome = rendimento.Convenio.Nome, Codigo = rendimento.Convenio.Codigo, Descricao = rendimento.Convenio.Descricao };
            ConvenioOrgao = rendimento.ConvenioOrgao == null ? null
                : new ConvenioOrgaoModel
                {
                    Id = rendimento.ConvenioOrgao.ID,
                    Nome = rendimento.ConvenioOrgao.Nome,
                    Codigo = rendimento.ConvenioOrgao.Codigo,
                    CNPJ = rendimento.ConvenioOrgao.CNPJ,
                    UF = rendimento.ConvenioOrgao.UF?.Sigla ?? ""
                };
            Uf = rendimento.Uf == null ? null : new UnidadeFederativaModel { Id = rendimento.Uf.ID, Nome = rendimento.Uf.Nome, Sigla = rendimento.Uf.Sigla };
            ValorRendimento = rendimento.ValorRendimento;
            Matricula = rendimento.Matricula;
            MargemDisponivel = rendimento.MargemDisponivel;
            MargemDisponivelCartao = rendimento.MargemDisponivelCartao;

            ContaCliente = rendimento.ContaCliente == null ? null : new ContaClienteExibicaoModel(rendimento.ContaCliente);
            ContaClienteRecebimento = rendimento.ContaCliente == null ? null : new ContaClienteExibicaoModel(rendimento.ContaClienteRecebimento);

            if (rendimento is RendimentoClienteInssDominio)
            {
                var rendimentoInss = rendimento as RendimentoClienteInssDominio;
                InssEspecieBeneficio = rendimentoInss.EspecieBeneficio == null ? null
                    : new InssEspecieBeneficioModel
                    {
                        Id = rendimentoInss.EspecieBeneficio.ID,
                        Descricao = rendimentoInss.EspecieBeneficio.Descricao,
                        Codigo = rendimentoInss.EspecieBeneficio.Codigo
                    };

                DataInscricaoBeneficio = rendimentoInss.DataInscricao;
            }
            else if (rendimento is RendimentoClienteSiapeDominio)
            {
                var rendimentoSiape = rendimento as RendimentoClienteSiapeDominio;
                SiapeTipoFuncional = rendimentoSiape.TipoFuncional == null ? null
                    : new SiapeTipoFuncionalModel
                    {
                        Id = rendimentoSiape.TipoFuncional.ID,
                        Descricao = rendimentoSiape.TipoFuncional.Descricao,
                        Codigo = rendimentoSiape.TipoFuncional.Codigo
                    };
                MatriculaInstituidor = rendimentoSiape.MatriculaInstituidor;
                PossuiRepresentacaoPorProcurador = rendimentoSiape.PossuiRepresentacaoPorProcurador;
                DataAdmissao = rendimentoSiape.DataAdmissao;
                NomeInstituidor = rendimentoSiape.NomeInstituidor;
                DataLiberacaoConsultaMargem = rendimentoSiape.DataLiberacaoConsultaMargem;
            }
        }
    }
}
