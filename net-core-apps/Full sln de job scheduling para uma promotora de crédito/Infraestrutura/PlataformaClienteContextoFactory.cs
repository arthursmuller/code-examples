using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infraestrutura
{
    [ExcludeFromCodeCoverage]
    public class PlataformaClienteContextoFactory : IDesignTimeDbContextFactory<PlataformaClienteContexto>
    {
        public PlataformaClienteContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlataformaClienteContexto>();
            return new PlataformaClienteContexto(optionsBuilder.Options);
        }
    }
}
