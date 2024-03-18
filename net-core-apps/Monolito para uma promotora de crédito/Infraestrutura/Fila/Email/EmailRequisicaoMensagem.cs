using System.Diagnostics.CodeAnalysis;

namespace Fila.Model.Email
{
    [ExcludeFromCodeCoverage]
    public class EmailRequisicaoMensagem
    {
        public string CodigoReferenciaMensagem { get; set; }
        public string[] Destinatarios { get; set; }
        public string[] Copias { get; set; }
        public string Mensagem { get; set; }
        public string Assunto { get; set; }
        public bool Prioritario { get; set; }
        public int IdEmailFornecedor { get; set; }
    }
}
