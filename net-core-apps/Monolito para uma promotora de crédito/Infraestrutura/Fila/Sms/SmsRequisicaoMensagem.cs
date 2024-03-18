﻿using System.Diagnostics.CodeAnalysis;

namespace Fila.Model.Sms
{
    [ExcludeFromCodeCoverage]
    public class SmsRequisicaoMensagem
    {
        public string CodigoReferenciaMensagem { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public string Mensagem { get; set; }
        public int IdSmsFornecedor { get; set; }
    }
}
