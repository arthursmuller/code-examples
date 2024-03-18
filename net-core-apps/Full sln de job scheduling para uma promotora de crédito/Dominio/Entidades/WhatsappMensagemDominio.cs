using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Dominio.Entidades
{
    public class WhatsappMensagemDominio : EntidadeBase
    {
        public string CodigoReferenciaMensagem { get; private set; }
        public Guid IdTemplate { get; private set; }
        public string NumeroTelefone { get; private set; }
        public string MensagemEnvio { get; set; }
        public string MensagemRetornoErro { get; set; }
        public int IdWhatsappFornecedor { get; private set; }

        public WhatsappFornecedorDominio WhatsappFornecedor { get; private set; }

        public WhatsappMensagemDominio() { }

        public WhatsappMensagemDominio(string codReferenciaMensagem
                                     , Guid idTamplate
                                     , string telefone
                                     , Dictionary<string, string> mensagem
                                     , int idWhatsappFornecedor)
        {
            CodigoReferenciaMensagem = codReferenciaMensagem;
            IdTemplate = idTamplate;
            NumeroTelefone = telefone;
            MensagemEnvio = JsonSerializer.Serialize(mensagem);
            IdWhatsappFornecedor = idWhatsappFornecedor;
        }

        public void RegistraMensagemRetornoErro(string mensagem)
        {
            MensagemRetornoErro = mensagem;
            DataAtualizacao = DateTime.Now;
        }
    }
}
