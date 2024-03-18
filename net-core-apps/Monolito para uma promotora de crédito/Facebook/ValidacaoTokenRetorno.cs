using System.Diagnostics.CodeAnalysis;

namespace LoginSocialFacebook
{
    [ExcludeFromCodeCoverage]
    public class ValidacaoTokenRetorno
    {
        public bool TokenValido { get; private set; }
        public string CodigoUsuario { get; private set; }

        public ValidacaoTokenRetorno(bool tokenValido, string codigoUsuario)
        {
            TokenValido = tokenValido;
            CodigoUsuario = codigoUsuario;
        }
    }
}
