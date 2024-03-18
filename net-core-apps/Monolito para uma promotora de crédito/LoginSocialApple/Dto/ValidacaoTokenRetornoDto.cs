using System.Diagnostics.CodeAnalysis;

namespace LoginSocialApple.Dto
{
    [ExcludeFromCodeCoverage]
    public class ValidacaoTokenRetornoDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
