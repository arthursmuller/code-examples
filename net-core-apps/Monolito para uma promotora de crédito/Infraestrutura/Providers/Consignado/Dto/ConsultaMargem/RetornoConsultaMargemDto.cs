using System.Collections.Generic;

namespace Infraestrutura.Providers.Consignado.Dto
{
    public class RetornoConsultaMargemDto
    {
        public bool Sucesso { get; set; }
        public string MensagemRetorno { get; set; }
        public IEnumerable<MargemSiapeItemDto> MargemSiapeItens { get; set; }
    }

    public class MargemSiapeItemDto
    {
        public string Orgao { get; set; }
        public string Matricula { get; set; }
        public string Instituidor { get; set; }
        public decimal ValorMaximoParcela { get; set; }
        public bool EmprestimoAutorizado { get; set; }
        public bool PortabilidadeAutorizada { get; set; }
        public string MensagemComplementar { get; set; }
    }
}
