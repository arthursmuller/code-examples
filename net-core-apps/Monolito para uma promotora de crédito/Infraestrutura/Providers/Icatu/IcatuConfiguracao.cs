using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.IcatuApi
{
	[ExcludeFromCodeCoverage]
	public class IcatuConfiguracao
    {
		public string BaseUrl { get; set; }
		public string CodigoEmpresa { get; set; }
		public string OcpApimSubscriptionKey { get; set; }
		public string Empresa { get; set; }
		public string LinhaNegocio { get; set; }
	}
}
