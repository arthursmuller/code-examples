using System;

namespace Infraestrutura.Providers.Cliente.Dto
{
    public class ClienteRendimentoDto
    {
        public string Matricula { get; set; }

        public string UF { get; set; }

        public DateTime? Admissao { get; set; }

        public decimal? Salario { get; set; }

        public string Banco { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public string TipoConta { get; set; }

        public string Conveniada { get; set; }

        public string Orgao { get; set; }

        public string EspecieINSS { get; set; }

        public short? FuncaoSIAPE { get; set; }

        public string MatriculaInstituidorSIAPE { get; set; }

        public string NomeInstituidorSIAPE { get; set; }

        public string NomeBanco { get; set; }

        public string NomeTipoConta { get; set; }

        public bool PermiteRecebimento { get; set; }
    }
}
