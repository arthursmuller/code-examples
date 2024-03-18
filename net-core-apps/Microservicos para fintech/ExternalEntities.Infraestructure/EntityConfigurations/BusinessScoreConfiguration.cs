using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;

namespace ExternalEntities.Infraestructure.EntityConfigurations
{
    public class BusinessScoreConfiguration : BaseConfiguration, IEntityTypeConfiguration<BusinessScore>
    {
        public void Configure(EntityTypeBuilder<BusinessScore> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder);

            modelBuilder.Property(m => m.Score)
                          .UsePropertyAccessMode(PropertyAccessMode.Field);
           
            modelBuilder.Property(m => m.Segment)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.PaymentCommitmentScore)
                 .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.ProfileScore)
                          .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
