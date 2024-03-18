using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dominio.Entidades;
using Infraestrutura;
using Infraestrutura.Enum;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Robo.Servicos
{
    [ExcludeFromCodeCoverage]
    public class InicializadorDb
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public InicializadorDb(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Migrate()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PlataformaClienteContexto>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PlataformaClienteContexto>())
                {
                    var zenviaStatusDetalhes = ValoresEnum<ZenviaStatusDetalhes>();
                    if (context.SituacoesEnvioDetalhes.Count() < zenviaStatusDetalhes.Count())
                    {
                        var novos = zenviaStatusDetalhes
                            .Select(e => new SituacaoEnvioDetalhesDominio((int)e, e.GetDescription()))
                            .Where(e => !context.SituacoesEnvioDetalhes.Any(t => t.ID == e.ID));

                        context.SituacoesEnvioDetalhes.AddRange(novos);
                        context.SaveChanges();
                    }

                    var zenviaStatus = ValoresEnum<ZenviaStatus>();
                    if (context.SituacoesEnvio.Count() < zenviaStatus.Count())
                    {
                        var novos = zenviaStatus
                            .Select(e => new SituacaoEnvioDominio((int)e, e.ToString()))
                            .Where(e => !context.SituacoesEnvio.Any(t => t.ID == e.ID));

                        context.SituacoesEnvio.AddRange(novos);
                        context.SaveChanges();
                    }

                    if (!context.Empresas.Any())
                    {
                        var empresa = new EmpresaDominio("PCF");
                        context.Empresas.Add(empresa);
                        context.SaveChanges();

                        var fornecedorEmail = new EmailFornecedorDominio(
                            "Bem Promotora", "clientehml@bempromotora.com.br", "matone@123", "smtp.office365.com", 587, true, empresa.ID);
                        context.EmailFornecedores.Add(fornecedorEmail);
                        context.SaveChanges();
                    }
                }
            }
        }
        
        private IEnumerable<T> ValoresEnum<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}