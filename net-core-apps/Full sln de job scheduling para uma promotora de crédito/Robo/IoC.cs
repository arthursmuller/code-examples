using Aplicacao.Fila;
using Aplicacao.Job;
using Aplicacao.Servico;
using Dominio.Repositorio;
using Infraestrutura.Providers;
using Infraestrutura.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using Robo.Servicos;

namespace Robo
{
    public static class IoC
    {
        private static ServiceProvider _provider;

        public static IServiceCollection ConfigurarDependencias(this IServiceCollection services)
        {
            services.AddScoped<IStatusRepositorio, StatusRepositorio>();
            services.AddScoped<InicializadorDb>();
            
            services.AddTransient<ConsumidorEmail>();
            services.AddTransient<ConsumidorSms>();
            services.AddTransient<ConsumidorWhatsApp>();
            services.AddTransient<ConsumidorTorpedoVoz>();

            services.AddScoped<JobAtualizarStatusSms>();
            services.AddScoped<JobAtualizarParentesco>();
            services.AddScoped<JobAtualizarProfissao>();
            services.AddScoped<InicializadorDb>();
            
            services.AddScoped<IProviderEmail, ProviderEmail>();
            services.AddScoped<IProviderZenvia, ProviderZenvia>();
            services.AddScoped<IProviderZenviaTotalVoice, ProviderZenviaTotalVoice>();

            services.AddTransient<EmailServico>();
            services.AddTransient<SmsServico>();
            services.AddTransient<WhatsAppServico>();
            services.AddTransient<TorpedoVozServico>();
            services.AddTransient<SeguroBemServico>();
            services.AddTransient<SeguroProfissaoServico>();

            _provider = services.BuildServiceProvider();

            return services;
        }

        public static T BuscarInstancia<T>()
        {
            return _provider.GetService<T>();
        }
    }
}