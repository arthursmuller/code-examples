using System.Text.RegularExpressions;

namespace Infraestrutura.Utils
{
    public static class StringExtensions
    {
        public static string CastToUpperSnakeCase(this string valor)
        {
            Regex pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");

            return string.Join("_", pattern.Matches(valor)).ToUpper();
        }
    }
}