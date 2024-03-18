using Aplicacao.Job;
using B.Agendador;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Aplicacao
{
    public class RoboServico : BackgroundService
    {
        private Microsoft.Extensions.Hosting.IHost _webHost;
        private readonly IBusControl _provedorFila;
        private readonly ILogger _logger;
        private readonly IAgendador _agendador;

        public RoboServico(IAgendador agendador, ILogger logger, IBusControl provedorFila)
        {
            _provedorFila = provedorFila;
            _logger = logger;
            _agendador = agendador;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await _agendador.Parar();

            await base.StopAsync(stoppingToken);
            await _webHost.StopAsync(stoppingToken);

            _logger.Information("Robo Parado");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _agendador
                .Agendar<JobAtualizarStatusSms>(6000)
                .Agendar<JobAtualizarProfissao>("0 0 20 ? * FRI *")
                .Agendar<JobAtualizarParentesco>("0 0 20 ? * FRI *")
                .Executar();

            _logger.Information("Robo iniciado");

            _webHost = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(c => c.Run(async s =>
                        await s.Response.WriteAsync("Plataforma Cliente Bem | Robô iniciado.", stoppingToken)));
                })
                .Build();

            await _webHost.StartAsync(stoppingToken);
        }
    }
}