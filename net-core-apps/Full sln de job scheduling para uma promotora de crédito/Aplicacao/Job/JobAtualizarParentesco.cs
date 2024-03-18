using Aplicacao.Servico;
using B.Agendador;
using Quartz;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Job
{
    public class JobAtualizarParentesco : Tarefa
    {
        private readonly ILogger _logger;
        private readonly SeguroBemServico _seguroBemServico;

        public JobAtualizarParentesco(ILogger logger, SeguroBemServico seguroBemServico)
        {
            _logger = logger;
            _seguroBemServico = seguroBemServico;
        }

        public override async Task Executar(IJobExecutionContext context)
        {
            try
            {
                await _seguroBemServico.AtualizarDados();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
        }
    }       
}
