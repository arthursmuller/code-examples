using System;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.AcessoDadosPessoais
{
    [ExcludeFromCodeCoverage]
    public class SolicitacaoAcessoDadosPessoaisModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomeMae { get; set; }
        public string Email { get; set; }
        public string TelefoneCompleto { get; set; }
        public string Motivo { get; set; }
        public bool NotificacaoEnviada { get; set; }
    }
}
