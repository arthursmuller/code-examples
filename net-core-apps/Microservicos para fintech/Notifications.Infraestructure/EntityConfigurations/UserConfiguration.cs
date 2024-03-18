using BrDateTimeUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.AggregatesModel.UserAggregate;
using System;

namespace Notifications.Infraestructure.EntityConfigurations
{
    public class UserConfiguration : BaseConfiguration, IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder, false);

            modelBuilder.Property(m => m.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Email)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Cellphone)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.HasData(new[]
            {
                new
                {
                    Id = 1,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Name = "Arthur Silva Muller",
                    Email = "arthur.muller@capwise.com.br",
                    Cellphone = ""
                },
                 new
                {
                    Id = 2,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Name = "Pablo Maino",
                    Email = "pablo@capwise.com.br",
                    Cellphone = ""
                },
                new
                {
                    Id = 3,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Name = "Arthur Decker",
                    Email = "arthur@capwise.com.br",
                    Cellphone = ""
                },
                new
                {
                    Id = 4,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Name = "",
                    Email = "",
                    Cellphone = ""
                },
            });
        }
    }
}
