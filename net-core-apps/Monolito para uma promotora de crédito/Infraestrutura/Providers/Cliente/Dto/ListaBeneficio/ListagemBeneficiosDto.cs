using System;

namespace Infraestrutura.Providers.Cliente.Dto.ListaBeneficio
{
    public class ListagemBeneficiosInssDto
    {
        public string NumeroBeneficio { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string NomeBeneficiario { get; set; }
        public SituacaoBeneficioDto SituacaoBeneficio { get; set; }
        public EspecieDto EspecieBeneficio { get; set; }
        public bool ConcessaoJudicial { get; set; }
        public DateTime? DataCessacaoBeneficio { get; set; }
        public string UfPagamento { get; set; }
        public TipoCreditoDto TipoCredito { get; set; }
        public string CbcIFPagadora { get; set; }
        public string AgenciaPagadora { get; set; }
        public string ContaCorrente { get; set; }
        public bool PossuiRepresentanteLegal { get; set; }
        public bool PossuiProcurador { get; set; }
        public bool PossuiEntidadeRepresentacao { get; set; }
        public PensaoAlimenticiaDto PensaoAlimenticia { get; set; }
        public string BloqueadoParaEmprestimo { get; set; }
        public DateTime? DataUltimaPericia { get; set; }
        public DateTime? DataDespachoBeneficio { get; set; }
        public decimal MargemDisponivel { get; set; }
        public decimal MargemDisponivelCartao { get; set; }
        public decimal ValorLimiteCartao { get; set; }
        public int QtdEmprestimosAtivosSuspensos { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string CpfRepresentanteLegal { get; set; }
        public string NomeRepresentanteLegal { get; set; }
        public DateTime? DataFimRepresentanteLegal { get; set; }
        public bool ElegivelEmprestimo { get; set; }
        public bool VerTudo { get; set; }
    }
}
