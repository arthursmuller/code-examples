using Aplicacao.Servico;
using B.Agendador;
using Quartz;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Job
{
    public class JobAtualizarProfissao : Tarefa
    {
        private readonly ILogger _logger;
        private readonly SeguroProfissaoServico _seguroProfissaoServico;

        public JobAtualizarProfissao(ILogger logger, SeguroProfissaoServico seguroProfissaoServico)
        {
            _logger = logger;
            _seguroProfissaoServico = seguroProfissaoServico;
        }

        public override async Task Executar(IJobExecutionContext context)
        {
            try
            {
                await _seguroProfissaoServico.AtualizarDados();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
        }
    }
}
