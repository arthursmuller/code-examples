using System;
using System.ComponentModel.DataAnnotations;
using Aplicacao.Model.ContaCliente;

namespace Aplicacao.Model.RendimentoCliente
{
    public class RendimentoClienteModel
    {
        [Required]
        public Dominio.Enum.Convenio Convenio { get; set; }
        public int? IdConvenioOrgao { get; set; }
        [Required]
        public int IdUf { get; set; }
        [Required]
        public decimal ValorRendimento { get; set; }
        public int? IdInssEspecieBeneficio { get; set; }
        public int? IdSiapeTipoFuncional { get; set; }
        [Required]
        public string Matricula { get; set; }
        public DateTime DataInscricaoBeneficio { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string MatriculaInstituidor { get; set; }
        public string NomeInstituidor { get; set; }
        public bool PossuiRepresentacaoPorProcurador { get; set; }
        public decimal? MargemDisponivel { get; set; }
        public decimal? MargemDisponivelCartao { get; set; }
        public int? IdMarinhaTipoFuncional { get; set; }
        public int? IdMarinhaCargo { get; set; }
        public int? IdAeronauticaTipoFuncional { get; set; }
        public int? IdAeronauticaCargo { get; set; }
        public ContaClienteModel ContaCliente { get; set; }
        public ContaClienteModel ContaClienteRecebimento { get; set; }
    }
}
