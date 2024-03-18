using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.AggregatesModel.NotificationAggregate;

namespace Notifications.Infraestructure.EntityConfigurations
{
    public class UserNotificationConfiguration : BaseConfiguration, IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder);

            modelBuilder.Property(m => m.Title)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Description)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Message)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Property(m => m.Viewed)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.HasOne(m => m.UserOwner)
                .WithMany()
                .HasForeignKey("_userId");

            modelBuilder.OwnsMany(
                c => c.Channels,
                u => {
                    u.WithOwner().HasForeignKey("_notificationId");
                    u.HasOne(e => e.Channel).WithMany().HasForeignKey("_channelId");
                    u.Property(b => b.CreatedDate).UsePropertyAccessMode(PropertyAccessMode.Field);
                    u.Property<int>("Id");
                    u.HasKey("Id");
                    u.ToTable("UserNotificationChannelRecords");
                });

            modelBuilder.OwnsMany(
                c => c.Recipients,
                u => {
                    u.WithOwner().HasForeignKey("_notificationId");
                    u.Property<int>("Id");
                    u.HasKey("Id");
                    u.ToTable("UserNotificationRecipients");
                });
        }
    }
}
