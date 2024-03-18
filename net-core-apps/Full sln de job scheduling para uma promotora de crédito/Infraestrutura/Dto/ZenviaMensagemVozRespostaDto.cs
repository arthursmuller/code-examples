namespace Infraestrutura.DTO.ZenviaMensagemVoz
{
    public partial class ZenviaMensagemVozRespostaDadosDto
    {
        public int Id { get; set; }
    }

    public class ZenviaMensagemVozRespostaDto
    {
        public int Status { get; set; }
        public bool Sucesso { get; set; }
        public int Motivo { get; set; }
        public string Mensagem { get; set; }
        public ZenviaMensagemVozRespostaDadosDto Dados { get; set; }
    }

}