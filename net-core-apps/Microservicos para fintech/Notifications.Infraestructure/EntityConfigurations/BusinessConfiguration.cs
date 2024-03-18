using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.AggregatesModel.BusinessAggregate;

namespace Notifications.Infraestructure.EntityConfigurations
{
    public class BusinessConfiguration : BaseConfiguration, IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder, false);

            modelBuilder.Property(m => m.Name)
                          .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Email)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Cellphone)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

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
