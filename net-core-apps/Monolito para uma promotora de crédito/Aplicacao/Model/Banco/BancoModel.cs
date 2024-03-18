using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Banco
{
    [ExcludeFromCodeCoverage]
    public class BancoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public bool PermitePortabilidade { get; set; }
    }
}
