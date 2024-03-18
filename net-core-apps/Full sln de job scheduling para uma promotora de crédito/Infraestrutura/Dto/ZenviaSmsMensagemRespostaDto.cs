using Infraestrutura.Enum;

namespace Infraestrutura.DTO.Zenvia
{
    public class ZenviaSmsMensagemRespostaDto
    {
        public ZenviaSmsMensagemRespostaStatusDto sendSmsResponse { get; set; }
    }

    public class ZenviaSmsMensagemStatusDto
    {
        public ZenviaSmsMensagemRespostaStatusDto getSmsStatusResp { get; set; }
    }

    public class ZenviaSmsMensagemRespostaStatusDto
    {
        public ZenviaStatus StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public ZenviaStatusDetalhes DetailCode { get; set; }
        public string DetailDescription { get; set; }
        public string MobileOperatorName { get; set;}
  }
}
