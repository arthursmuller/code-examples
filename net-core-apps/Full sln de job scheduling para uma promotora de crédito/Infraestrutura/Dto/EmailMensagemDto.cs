namespace Infraestrutura.DTO.Email
{
    public class EmailMensagemDto
    {
        public string Destinatario { get; set; }
        public string Copia { get; set; }
        public string Assunto { get; set; }
        public bool Prioritario { get; set; }
        public string Corpo { get; set; }

        public int Porta { get; set; }
        public string Host { get; set; }
        public string Senha { get; set; }
        public string Usuario { get; set; }
        public string NomeExibicao { get; set; }
        public bool Ssl { get; set; }
  }
}
