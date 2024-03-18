using B.Agendador;
using Quartz;
using Serilog;
using Aplicacao.Servico;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Job
{
    public class JobAtualizarStatusSms : Tarefa
    {
        private readonly ILogger _logger;
        private readonly SmsServico _smsServico;

        public JobAtualizarStatusSms(ILogger logger, SmsServico smsServico)
        {
            _logger = logger;
            _smsServico = smsServico;
        }

        public override async Task Executar(IJobExecutionContext context)
        { 
            try
            {
                await _smsServico.AtualizarRequisicoesEmAberto();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
        }
    }
}
