using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Unico
{

	[ExcludeFromCodeCoverage]
	public class UnicoConfiguracao
	{
		public string UrlBaseApi { get; set; }
		public string AuthUrlBaseApi { get; set; }
		public string UrlProcessos { get; set; }
		public string AuthUrlGerarToken { get; set; }
		public string ChaveApi { get; set; }
		public string ServiceAccount { get; set; }
		public string ChavePrivada { get; set; }
		public string Tenant { get; set; }
		public int TokenAcessoValidadeEmSegundos { get; set; }
	}
}