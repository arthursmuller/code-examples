using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;

namespace ExternalEntities.Infraestructure.EntityConfigurations
{
    public class BusinessConfiguration : BaseConfiguration, IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder, false);

            modelBuilder.Property(m => m.Cnpj)
                          .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.HasMany(u => u.Scores).WithOne();
            modelBuilder.HasMany(u => u.PayiedAnalysisRequests).WithOne(e => e.Business);

            modelBuilder.OwnsMany(
             c => c.Owners,
             u =>
             {
                 u.WithOwner().HasForeignKey("_businessId");
                 u.HasOne(e => e.User).WithMany().HasForeignKey("_userId");
                 u.Property<int>("Id");
                 u.HasKey("Id");
                 u.ToTable("BusinessOwners");
             });
        }
    }
}
