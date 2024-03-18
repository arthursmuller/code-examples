using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Unico.DTO
{
	[ExcludeFromCodeCoverage]
	public class UnicoRequisicaoCriarProcessoDto
	{
		public UnicoRequisicaoCriarProcessoSubjectDto subject { get; set; }
		public bool onlySelfie { get; set; }
		public string webHookUrl { get; set; }
		public string webHookSecret { get; set; }
		public bool withMask { get; set; }
		public string imagebase64 { get; set; }
	}

	[ExcludeFromCodeCoverage]
	public class UnicoRequisicaoCriarProcessoSubjectDto
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string Gender { get; set; }
		public string BirthDate { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}
}