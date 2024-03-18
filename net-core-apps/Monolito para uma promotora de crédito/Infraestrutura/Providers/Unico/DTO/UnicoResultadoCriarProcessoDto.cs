using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Unico.DTO
{
	[ExcludeFromCodeCoverage]
	public class UnicoResultadoCriarProcessoDto
	{
		public string Id { get; set; }
		public UnicoResultadoErroDto Error { get; set; }
	}

	[ExcludeFromCodeCoverage]
	public class UnicoResultadoErroDto
	{
		public int Code { get; set; }
		public string Description { get; set; }
	}
}