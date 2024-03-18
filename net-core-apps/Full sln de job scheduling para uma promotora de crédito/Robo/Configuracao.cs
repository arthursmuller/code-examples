using Aplicacao.Fila;
using Aplicacao.Model;
using Aplicacao.Servico;
using B.Agendador;
using B.Configuracao;
using B.Logs;
using B.Logs.Configurations;
using B.Mensagens.Implementacoes;
using B.Mensagens.Interfaces;
using B.Repositorio;
using B.WhatsApp;
using Dominio.Abstracoes;
using Fila.Model.Email;
using Fila.Model.Sms;
using Fila.Model.TorpedoVoz;
using Fila.Model.WhatsApp;
using GreenPipes;
using Infraestrutura;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Robo
{
    [ExcludeFromCodeCoverage]
    public static class Configuracao
    {
        private static IConfigurationRoot ConfigurationRoot { get; set; }

        public static IServiceCollection ConfigurarServico(this IServiceCollection services)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory);
            var builder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(basePath, "appsettings.json"), optional: false);

            ConfigurationRoot = builder.Build();

            var origemConfiguracao = ConfigurationRoot.GetSection("ConfiguracaoOrigem").Get<ConfiguracaoOrigem>();
            string ambiente = origemConfiguracao?.Ambiente?.ToUpper();

            definirConexaoSql(origemConfiguracao);

            var configuracao = services.AdicionarConfiguracoes(origemConfiguracao);

            var conn = configuracao.BuscarParametro("ROBO-BD-Valor");
            services.AddDbContext<PlataformaClienteContexto>(o => o.UseSqlServer(conn), ServiceLifetime.Transient);

            var connBack = configuracao.BuscarParametro("BACK-BD-Valor");
            services.AddDbContext<ProdutoClienteContexto>(o => o.UseSqlServer(connBack), ServiceLifetime.Transient);

            IEnumerable<ConfiguracaoConexao> conexoes = new List<ConfiguracaoConexao>{
                new ConfiguracaoConexao{
                    Nome = configuracao.BuscarParametro("ROBO-BD-Nome"),
                    Valor = conn
                }
            };

            services
                .ConfigureSerilog(LogType.Azure, configuracao)
                .AddSingleton(conexoes)
                .AddScoped<IBemMensagens, BemMensagens>()
                .AddScoped<IConexaoBanco, ConexaoBanco>()
                .ConfigurarDependencias();

            services.AddHttpClient<IIcatuApiServico, IcatuApiServico>(client =>
            {
                var baseUri = configuracao.BuscarParametro("ICATU-API-BASE-URL"); 
                var codigoEmpresa = configuracao.BuscarParametro("ICATU-API-CODIGO-EMPRESA");
                var subcriptionKey = configuracao.BuscarParametro("ICATU-API-SUBSCRIPTION-KEY"); 
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Add("CodigoEmpresa", codigoEmpresa);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subcriptionKey);
              
            });

            var configuracaoWhatsApp = new ConfiguracaoBWhatsApp
            {
                NumeroOrigem = configuracao.BuscarParametro("configuracao_whatsapp_numeroorigem"),
                Token = configuracao.BuscarParametro("configuracao_whatsapp_token"),
                Url = configuracao.BuscarParametro("configuracao_whatsapp_url")
            };

            services.ConfigurarBWhatsApp(configuracaoWhatsApp);
            services
                .ConfigureSerilog(LogType.Azure, configuracao)
                .AddSingleton(conexoes)
                .AddScoped<IBemMensagens, BemMensagens>()
                .AddScoped<IConexaoBanco, ConexaoBanco>()
                .ConfigurarDependencias();

            string filaSMS = $"ROBO-FILA-{ambiente}/SMS";
            string filaEmail = $"ROBO-FILA-{ambiente}/Email";
            string filaWhatsApp = $"ROBO-FILA-{ambiente}/WHATSAPP";
            string filaTorpedoVoz = $"ROBO-FILA-{ambiente}/TorpedoVoz";

            services
                //.AddApplicationInsightsTelemetryWorkerService()
                .AddMassTransit(x =>
                {
                    x.AddBus(provider => Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                    {
                        cfg.Host(configuracao.BuscarParametro("robo-fila"));

                        cfg.UseCircuitBreaker(cb =>
                            {
                                cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                                cb.TripThreshold = 15;
                                cb.ActiveThreshold = 10;
                                cb.ResetInterval = TimeSpan.FromMinutes(5);
                            });

                        cfg.ReceiveEndpoint(filaSMS, e =>
                        {
                            e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(1)));

                            e.Handler<SmsRequisicaoMensagem>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorSms>().Processar(context.Message);
                            });
                            e.Handler<Fault<SmsRequisicaoMensagem>>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorSms>().ProcessarErro(context.Message.Message);
                            });
                        });

                        cfg.ReceiveEndpoint(filaEmail, e =>
                        {
                            e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(1)));

                            e.Handler<EmailRequisicaoMensagem>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorEmail>().Processar(context.Message);
                            });
                            e.Handler<Fault<EmailRequisicaoMensagem>>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorEmail>().ProcessarErro(context.Message.Message);
                            });
                        });

                        cfg.ReceiveEndpoint(filaWhatsApp, e =>
                        {
                            e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(1)));

                            e.Handler<WhatsAppRequisicaoMensagem>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorWhatsApp>().Processar(context.Message);
                            });
                            e.Handler<Fault<WhatsAppRequisicaoMensagem>>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorWhatsApp>().ProcessarErro(context.Message.Message);
                            });
                        });

                        cfg.ReceiveEndpoint(filaTorpedoVoz, e =>
                        {
                            e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(1)));

                            e.Handler<TorpedoVozRequisicaoMensagem>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorTorpedoVoz>().Processar(context.Message);
                            });
                            e.Handler<Fault<TorpedoVozRequisicaoMensagem>>(async context =>
                            {
                                await IoC.BuscarInstancia<ConsumidorTorpedoVoz>().ProcessarErro(context.Message.Message);
                            });
                        });
                    }));
                });

            services.AddMassTransitHostedService();
            services.AddAgendador();

            return services;
        }

        private static void definirConexaoSql(ConfiguracaoOrigem origemConfiguracao)
        {
            if (origemConfiguracao?.Tipo == "SQL")
            {
                var conexaoOrigem = ConnectionsDecryptor.GetPlainConnections().ToList();
                origemConfiguracao.Origem = conexaoOrigem.FirstOrDefault().Valor;
            }
        }
    }
}