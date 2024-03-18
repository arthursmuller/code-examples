using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;

namespace ExternalEntities.Infraestructure.EntityConfigurations
{
    public class UserScoreConfiguration : BaseConfiguration, IEntityTypeConfiguration<UserScore>
    {
        public void Configure(EntityTypeBuilder<UserScore> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder);

            modelBuilder.Property(m => m.Score)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.FullAnalysisDone)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            modelBuilder.OwnsMany(eqd2 => eqd2.History).ToTable("ScoreFilterHistory");
        }
    }
}
