using System;

namespace Infraestrutura.Providers.Cliente.Dto
{
    public class ClienteDadosBasicosDto
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Mae { get; set; }
        public DateTime? Nascimento { get; set; }
        public string CicCgc { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public string CidadeNascimento { get; set; }
        public string UFNascimento { get; set; }
        public string CodigoEstadoCivil { get; set; }
        public string DescricaoEstadoCivil { get; set; }
        public string CodigoRegimeCasamento { get; set; }
        public string CodigoGrauInstrucao { get; set; }
        public string DescricaoGrauInstrucao { get; set; }
        public string Pai { get; set; }
        public bool DeficienteVisual { get; set; }
    }
}
