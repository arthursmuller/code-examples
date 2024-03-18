using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;

namespace ExternalEntities.Infraestructure.EntityConfigurations
{
    public class AnalysisRequestsConfiguration : BaseConfiguration, IEntityTypeConfiguration<AnalysisRequest>
    {
        public void Configure(EntityTypeBuilder<AnalysisRequest> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder);

            modelBuilder
                .Property(m => m.Cpf)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder
                .Property(m => m.Cost)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder
                .Property(m => m.IsCompleted)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
