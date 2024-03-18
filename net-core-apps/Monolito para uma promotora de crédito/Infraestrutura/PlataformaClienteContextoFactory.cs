using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura
{
    [ExcludeFromCodeCoverage]
    public class PlataformaClienteContextoFactory : IDesignTimeDbContextFactory<PlataformaClienteContexto>
    {
        public PlataformaClienteContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlataformaClienteContexto>();
            optionsBuilder.UseSqlServer(x => x.UseNetTopologySuite());
            return new PlataformaClienteContexto(optionsBuilder.Options);
        }
    }
}
