using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using Assembly = System.Reflection.Assembly;

namespace TesteArquitetura
{
    public class TesteArquitetura
    {
        private static readonly Architecture Arquitetura =
            new ArchLoader().LoadAssemblies(
                Assembly.LoadFrom("Api.dll"),
                Assembly.LoadFrom("Aplicacao.dll"),
                Assembly.LoadFrom("Dominio.dll"),
                Assembly.LoadFrom("Infraestrutura.dll"))
            .Build();

        private readonly IObjectProvider<IType> camadaAPI =
            Types().That().ResideInAssembly("Api").As("API");

        private readonly IObjectProvider<IType> camadaAplicacao =
            Types().That().ResideInAssembly("Aplicacao").As("Aplicacao");

        private readonly IObjectProvider<IType> camadaDominio =
            Types().That().ResideInAssembly("Dominio").As("Dominio");

        private readonly IObjectProvider<IType> camadaInfraestrutura =
            Types().That().ResideInAssembly("Infraestrutura").As("Infraestrutura");

        [Fact]
        public void ApiNaoPodeAcessarDominio()
        {
            var regraCamadas = Types()
                .That()
                .Are(camadaAPI)
                .Should()
                .NotDependOnAny(camadaDominio)
                .Because("A camada da API nao pode acessar o dominio");

            regraCamadas.Check(Arquitetura);
        }

        [Fact]
        public void DominioNaoPodeAcessarAplicacao()
        {
            var regraCamadas = Types()
                .That()
                .Are(camadaDominio)
                .Should()
                .NotDependOnAny(camadaAplicacao)
                .Because("A camada de dominio nao pode acessar a aplicacao");

            regraCamadas.Check(Arquitetura);
        }

        [Fact]
        public void DominioNaoPodeAcessarInfraestrutura()
        {
            var regraCamadas = Types()
                .That()
                .Are(camadaDominio)
                .Should()
                .NotDependOnAny(camadaInfraestrutura)
                .Because("A camada de dominio nao pode acessar a infraestrutura");

            regraCamadas.Check(Arquitetura);
        }
    }
}
