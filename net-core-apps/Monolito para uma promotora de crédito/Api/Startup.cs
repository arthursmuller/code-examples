using Aplicacao;
using Aplicacao.Filters;
using B.Autenticacao;
using B.Comunicacao;
using B.Configuracao;
using B.Logs;
using B.Logs.Configurations;
using B.Logs.Contracts;
using B.Logs.Middleware;
using B.Logs.Models;
using B.Mensagens.Implementacoes;
using B.Mensagens.Interfaces;
using B.Repositorio;
using B.Swagger.Extensions;
using B.Web.Filters;
using B.Facetec;
using FluentValidation.AspNetCore;
using Infraestrutura;
using Infraestrutura.Autenticacao;
using Infraestrutura.Geolocalizacao;
using Infraestrutura.Inicializacao;
using Infraestrutura.Providers;
using Infraestrutura.Providers.Dto;
using Infraestrutura.RedesSociais;
using LoginSocialApple;
using LoginSocialFacebook;
using MassTransit;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Infraestrutura.Providers.Unico;
using Infraestrutura.Providers.IcatuApi;
using Infraestrutura.Providers.Kaledo;


namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        public static bool MapperRegistred { get; set; }
        private static object _lockMapper = new object();

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var origemConfiguracao = this.Configuration.GetSection("ConfiguracaoOrigem").Get<ConfiguracaoOrigem>();

            definirConexaoSql(origemConfiguracao);

            var configuracao = services.AdicionarConfiguracoes(origemConfiguracao);

            var conn = configuracao.BuscarParametro("BACK-BD-Valor");
            services.AddDbContext<PlataformaClienteContexto>(o => o.UseSqlServer(conn, x => x.UseNetTopologySuite()));

            IEnumerable<ConfiguracaoConexao> conexoes = new List<ConfiguracaoConexao>
            {
                new ConfiguracaoConexao
                {
                    Nome = configuracao.BuscarParametro("BACK-BD-Nome"),
                    Valor = configuracao.BuscarParametro("BACK-BD-Valor"),
                },
            };

            var configuracaoAutenticacao = new ConfiguracaoAutenticacao
            {
                ChaveJwt = configuracao.BuscarParametro("chave_jwt"),
                SegundosParaExpirarToken = Convert.ToInt32(configuracao.BuscarParametro("segundos-para-expirar-token")),
                SegundosParaAtualizarToken = Convert.ToInt32(configuracao.BuscarParametro("segundos-para-atualizar-token")),
            };

            services.ConfigurarAutenticacao(configuracaoAutenticacao.ChaveJwt);

            services.Configure<WebServiceClientOptions>(a =>
            {
                a.AccountId = int.Parse(configuracao.BuscarParametro("maxmind-accountid"));
                a.LicenseKey = configuracao.BuscarParametro("maxmind-licensekey");
                a.Timeout = int.Parse(configuracao.BuscarParametro("maxmind-timeout"));
                a.Host = configuracao.BuscarParametro("maxmind-host");
            });

            services.AddHttpClient<WebServiceClient>();

            var configuracaoArquivo = new ConfiguracaoArquivo()
            {
                ConnectionString = configuracao.BuscarParametro("configuracaoarquivoazure_connectionstring"),
                ContainerName = configuracao.BuscarParametro("configuracaoarquivoazure_containername"),
                CaminhoPasta = configuracao.BuscarParametro("configuracaoarquivoazure_caminhopasta"),
                Url = configuracao.BuscarParametro("configuracaoarquivoazure_url")
            };

            var configuracaoProviders = new ConfiguracaoProviders()
            {
                AuthApi = configuracao.BuscarParametro("auth-api"),
                ConsignadoApi = configuracao.BuscarParametro("consignado-api"),
                ClienteApi = configuracao.BuscarParametro("cliente-api"),
                BemApi = configuracao.BuscarParametro("bem-api"),
                IcatuApi = configuracao.BuscarParametro("icatu-api"),
                Paperless = new ConfiguracaoProviderPaperless
                {
                    PaperlessApi = configuracao.BuscarParametro("paperless-api"),
                    PrefixoTermoAutorizacao = configuracao.BuscarParametro("paperless-prefixotermoautorizacao"),
                    ExtensaoDocumentoTermo = configuracao.BuscarParametro("paperless-extensaodocumentotermoautorizacao"),
                },
                ConsignadoUsuario = configuracao.BuscarParametro("usuario-consignado"),
                ConsignadoSenha = configuracao.BuscarParametro("senha-consignado")
            };

            services
                .AddMassTransit(x =>
                {
                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(configuracao.BuscarParametro("robo-fila"));
                    });
                })
                .AddMassTransitHostedService();

            var redesSociaisConfiguracao = new RedesSociaisConfiguracao
            {
                WhatsApp = new WhatsAppConfiguracao { LinkGeradorFormatado = configuracao.BuscarParametro("whatsapp-link-gerador-formatado") },
                Google = new GoogleConfiguracao
                {
                    ClientId = configuracao.BuscarParametro("configuracaologinsocial_google_clientid"),
                    EmitentesValidos = configuracao.BuscarParametroArray("configuracaologinsocial_google_emitentesvalidos")
                }
            };

            var geolocalizacaoConfiguracao = new GeolocalizacaoConfiguracao
            {
                IdReferenciaEspacial = Convert.ToInt32(configuracao.BuscarParametro("id-referencia-espacial"))
            };

            services
                .AddApplicationInsightsTelemetry()
                .AddSingleton(conexoes)
                .AddSingleton(origemConfiguracao)
                .AddSingleton(configuracaoArquivo)
                .AddSingleton(configuracaoProviders)
                .AddSingleton(redesSociaisConfiguracao)
                .AddSingleton(geolocalizacaoConfiguracao)
                .AddSingleton(configuracaoAutenticacao)
                .ConfigureSerilog(LogType.Azure, configuracao)
                .ConfigurarConecta(TipoCliente.HttpClientFactory, configuracao)
                .ConfigurarFacetec(configuracao)
                .ConfigurarLoginSocialFacebook(configuracao)
                .ConfigurarLoginSocialApple(configuracao)
                .ConfigurarKaledo(configuracao)
                .ConfigurarIcatu(configuracao)
                .ConfigurarUnico(configuracao)
                .AddScoped<IBemMensagens, BemMensagens>()
                .AddScoped<ILogUsuario, LogUsuario>()
                .AddScoped<IConexaoBanco, ConexaoBanco>()
                .AddScoped<FiltroLogRequisicao>()
                .ConfigurarDependencias(configuracao)
                .AddBSwaggerGen("Plataforma Cliente Final")
                .AddCors(o => o.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));

            services
                .AddControllers(opt =>
                {
                    opt.Filters.Add<GlobalException>();
                    opt.Filters.Add<ValidacaoModelAttribute>();
                    opt.Filters.Add<FiltroAtualizacaoToken>();
                });

            services
                .AddMvc().AddFluentValidation(fv => fv.ImplicitlyValidateChildProperties = true);

            lock (_lockMapper)
            {
                if (!MapperRegistred)
                {
                    if (Env.EnvironmentName == "Teste")
                        AutoMapper.Mapper.Reset();

                    Adapter.Mapear();
                    MapperRegistred = true;
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseBSwagger();

                var inicializadorDb = provider.GetService<InicializadorDb>();
                inicializadorDb.Migrate();
                inicializadorDb.SeedData();
            }

            app
                .UseCors()
                .UseMiddleware<LogGeral>()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        //TODO: Debito PCF-307: Remover este m�todo quando as configura��es estiverem 100% no Azure
        private void definirConexaoSql(ConfiguracaoOrigem origemConfiguracao)
        {
            if (origemConfiguracao?.Tipo == "SQL")
            {
                var conexaoOrigem = ConnectionsDecryptor.GetPlainConnections().FirstOrDefault();
                origemConfiguracao.Origem = conexaoOrigem?.Valor;
            }
        }
    }
}