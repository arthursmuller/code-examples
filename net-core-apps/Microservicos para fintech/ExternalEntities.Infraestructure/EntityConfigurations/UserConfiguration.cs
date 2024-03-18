using BrDateTimeUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using System;

namespace ExternalEntities.Infraestructure.EntityConfigurations
{
    public class UserConfiguration : BaseConfiguration, IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder, false);

            modelBuilder.Property(m => m.Cpf).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.TotalDelinquencyAmount).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.TotalCurrentDebitAmount).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.TotalCurrentDebitDelinquentAmount).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.TotalOverdrawnAmount).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.TotalExternalCreditAvailable).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(m => m.TotalFinancialOperations).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.BirthDate).UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.HasMany(u => u.Scores).WithOne();
            modelBuilder.OwnsOne(u => u.Address).ToTable("UserAddress");

            modelBuilder.OwnsMany(
                c => c.QuodDataRecords,
                qdr => 
                {
                    qdr.OwnsOne(
                       qdr2 => qdr2.ExternalQuodData,
                       eqd =>
                       {
                           eqd.OwnsOne(eqd2 => eqd2.Score).ToTable("QuodDataScore");
                           eqd.OwnsOne(eqd2 => eqd2.Negative).ToTable("QuodDataNegative");
                           eqd.OwnsOne(eqd2 => eqd2.CurrentAddress).ToTable("QuodDataCurrentAddress");
                           eqd.OwnsMany(eqd2 => eqd2.Protests).ToTable("QuodDataProtests");
                           eqd.OwnsMany(eqd2 => eqd2.Addresses).ToTable("QuodDataAddresses");
                           eqd.OwnsMany(eqd2 => eqd2.Emails).ToTable("QuodDataEmails");
                           eqd.OwnsMany(eqd2 => eqd2.PhoneNumbers).ToTable("QuodDataPhoneNumbers");
                           eqd.OwnsMany(eqd2 => eqd2.MobilePhoneNumbers).ToTable("QuodDataMobilePhoneNumbers");
                       });
                    qdr.ToTable("QuodDataRecords");
                });

            modelBuilder.Navigation(e => e.QuodDataRecords).AutoInclude(false);
            modelBuilder.Ignore(c => c.IsDenied);

            modelBuilder.HasData(new[]
            {
                new
                {
                    Id = 1,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Cpf = "02981603078",
                },
                new
                {
                    Id = 2,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Cpf = "77735936044",
                },
                new
                {
                    Id = 3,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Cpf = "84509848072",
                },
                new
                {
                    Id = 4,
                    UpdateDate = DateTime.Now.Brasilia(),
                    CreatedDate = DateTime.Now.Brasilia(),
                    Cpf = "71096627051",
                },
            });
        }
    }
}
