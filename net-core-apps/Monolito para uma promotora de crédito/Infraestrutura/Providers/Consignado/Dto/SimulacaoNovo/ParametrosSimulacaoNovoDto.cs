using System;
using System.ComponentModel.DataAnnotations;

namespace Infraestrutura.Providers.Consignado.Dto
{
    public class ParametrosSimulacaoNovoDto
    {
        [Required(ErrorMessage = "Conveniada é obrigatória.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Conveniada inválida. Verifique a quantidade de dígitos.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Conveniada inválida. Verifique se a mesma é composta somente por números.")]
        public string Conveniada { get; set; }

        public decimal? ValorOperacao { get; set; }

        public decimal? Prestacao { get; set; }

        [MaxLength(4, ErrorMessage = "Plano inválido. Verifique a quantidade de dígitos.")]
        public string Plano { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "Prazo inválido. Verifique a quantidade de dígitos.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Prazo inválido. Contém letras.")]
        public string Prazo { get => _prazo; set => _prazo = value?.PadLeft(3, '0'); }
        private string _prazo;

        public int[] Prazos { get; set; }

        public DateTime? DataNascimento { get; set; }

        public bool RetornarSomenteOperacoesViaveis { get; set; }

        public long? Proposta { get; set; }
    }
}
