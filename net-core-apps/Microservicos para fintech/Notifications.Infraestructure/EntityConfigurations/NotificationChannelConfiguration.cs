using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.AggregatesModel.NotificationAggregate;

namespace Notifications.Infraestructure.EntityConfigurations
{
    public class NotificationChannelConfiguration : IEntityTypeConfiguration<NotificationChannel>
    {
        public void Configure(EntityTypeBuilder<NotificationChannel> modelBuilder)
        {
            modelBuilder.ToTable("NotificationChannels");

            modelBuilder.HasKey(o => o.Id);

            modelBuilder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            modelBuilder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.HasData(NotificationChannel.List());
        }
    }
}
