using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signature.Domain.AggregatesModel.SignatureAggregate;

namespace Signature.Infraestructure.EntityConfigurations
{
    public class SignatureConfiguration : BaseConfiguration, IEntityTypeConfiguration<SignatureInformation>
    {
        public void Configure(EntityTypeBuilder<SignatureInformation> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder);
            
            modelBuilder.Property(e => e.UserId)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.UserIdentification)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.UserName)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.UserEmail)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.UserCellphone)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.UserIpAddress)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.ProductDatabaseRecordId)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.ProductTypeId)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.CertificateUrl)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.DocumentUrl)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.DocumentFileExtension)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.CertificateUrl)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.UserPictureUrl)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.SignatureDrawingUrl)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.Latitude)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.Longitude)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.CertificateGenerationDate)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.City)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.State)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.Country)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Property(e => e.PostalCode)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
