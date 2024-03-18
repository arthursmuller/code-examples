using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Unico.DTO
{
	[ExcludeFromCodeCoverage]
	public class UnicoResultadoAutenticarDto
	{
		public string access_token { get; set; }
		public int expires_in { get; set; }
		public string token_type { get; set; }
	}
}