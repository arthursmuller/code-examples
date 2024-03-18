using System.Threading.Tasks;
using Fila.Model.Sms;
using Serilog;
using Aplicacao.Servico;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Fila
{
    public class ConsumidorSms
    {
        private readonly SmsServico _smsServico;
        private readonly ILogger _logger;

        [ExcludeFromCodeCoverage]
        public ConsumidorSms(SmsServico smsServico, ILogger logger)
        {
            _smsServico = smsServico;
            _logger = logger;
        }

        public async Task Processar(SmsRequisicaoMensagem mensagem)
        {
            try{
                await _smsServico.ProcessarRequisicao(mensagem);
            }
            catch(Exception ex){
                _logger.Error($"Erro ao processar requisição de SMS {mensagem.CodigoReferenciaMensagem}", ex.Message, ex.StackTrace);
            }
        }

        public async Task ProcessarErro(SmsRequisicaoMensagem mensagem)
        {
            _logger.Error($"Erro ao processar requisição de SMS {mensagem.CodigoReferenciaMensagem}", mensagem);
        }
    }
}