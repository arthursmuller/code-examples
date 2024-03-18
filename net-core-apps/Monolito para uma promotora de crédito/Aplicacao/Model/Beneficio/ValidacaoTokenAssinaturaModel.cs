using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Beneficio
{
    [ExcludeFromCodeCoverage]
    public class ValidacaoTokenAssinaturaModel
    {
        public bool TokenEhValido { get; set; }

        public string ChaveAutorizacao { get; set; }

        public ValidacaoTokenAssinaturaModel(bool tokenEhValido, string chaveAutorizacao)
        {
            TokenEhValido = tokenEhValido;
            ChaveAutorizacao = chaveAutorizacao;
        }
    }
}
