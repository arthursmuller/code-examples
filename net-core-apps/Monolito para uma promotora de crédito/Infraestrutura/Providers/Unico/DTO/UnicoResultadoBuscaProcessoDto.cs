using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Unico.DTO
{
	[ExcludeFromCodeCoverage]
	public class UnicoResultadoBuscaProcessoDto
	{
		public bool FaceMatch { get; set; }
		public bool HasBiometry { get; set; }
		public string Id { get; set; }
		public bool Liveness { get; set; }
		public int OCRCode { get; set; }
		public int Score { get; set; }
		public int Status { get; set; }
	}
}