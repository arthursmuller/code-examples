namespace Infraestrutura.DTO.ZenviaTorpedoVoz
{
    public partial class ZenviaTorpedoVozRespostaDadosDto
    {
        public int Id { get; set; }
    }

    public class ZenviaTorpedoVozRespostaDto
    {
        public int Status { get; set; }
        public bool Sucesso { get; set; }
        public int Motivo { get; set; }
        public string Mensagem { get; set; }
        public ZenviaTorpedoVozRespostaDadosDto Dados { get; set; }
    }

}