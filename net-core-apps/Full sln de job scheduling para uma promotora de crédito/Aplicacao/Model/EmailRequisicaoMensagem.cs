namespace Fila.Model.Email
{
    public class EmailRequisicaoMensagem
    {
        public string CodigoReferenciaMensagem { get; set; }
        public string[] Destinatarios { get; set; }
        public string[] Copias { get; set; }
        public string Mensagem { get; set; }
        public string Assunto { get; set; }
        public bool Prioritario { get; set; }
        public int IdEmailFornecedor { get; set; }
        
        public string Destinario { get => string.Join(", ", Destinatarios ?? new string[] {}); }
        public string Copia { get => string.Join(", ", Copias ?? new string[] {}); }
    }
}
