using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.AcessoDadosPessoais
{
    [ExcludeFromCodeCoverage]
    public class SolicitacaoAcessoDadosPessoaisEnvioModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string NomeMae { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required]
        public SolicitacaoAcessoDadosPessoaisTelefoneEnvioModel TelefoneCompleto { get; set; }
    }
}
