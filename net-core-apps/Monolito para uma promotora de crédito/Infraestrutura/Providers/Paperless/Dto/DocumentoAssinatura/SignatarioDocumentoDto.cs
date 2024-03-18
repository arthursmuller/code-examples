using System;

namespace Infraestrutura.Providers.Paperless.Dto
{
    public class SignatarioDocumentoDto
    {
        public bool Assinado { get; set; }
        public string Email { get; set; }
        public string DDD { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? Nascimento { get; set; }
        public TipoDeAutenticacaoAssinaturaDocumentoDto TipoDeAutenticacao { get; set; }
        public bool EnvioPorSms { get; set; }
        public bool EnvioPorEmail { get; set; }
        public int SequenciaDaAssinatura { get; set; }
        public string Cidade { get; set; }
        public string TxLink { get; set; }
        public SignatarioEvidenciaDto Evidencias { get; set; }
    }
}
