using Fila.Model.WhatsApp;
using Aplicacao.Servico;
using Serilog;
using System.Threading.Tasks;
using System;
using B.WhatsApp;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Fila
{
    [ExcludeFromCodeCoverage]
    public class ConsumidorWhatsApp
    {
        private readonly WhatsAppServico _whatsAppServico;
        private readonly ILogger _logger;

        public ConsumidorWhatsApp(WhatsAppServico whatsAppServico, ILogger logger)
        {
            _whatsAppServico = whatsAppServico;
            _logger = logger;
        }

        public async Task Processar(WhatsAppRequisicaoMensagem mensagem){
            try{
                await _whatsAppServico.EnviarMensagem(mensagem);
            }
            catch(Exception ex){
                _logger.Error($"Erro ao processar requisição de Whatsapp {mensagem.CodigoReferenciaMensagem}", ex.Message, ex.StackTrace);
            }
        }

        public async Task ProcessarErro(WhatsAppRequisicaoMensagem mensagem)
            => _logger.Error("Erro ao processar requisição de envio whatsapp", mensagem);
    }
}
