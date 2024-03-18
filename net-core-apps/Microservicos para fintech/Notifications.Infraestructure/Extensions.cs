using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Domain.AggregatesModel.BusinessAggregate;
using Notifications.Domain.AggregatesModel.NotificationAggregate;
using Notifications.Domain.AggregatesModel.UserAggregate;
using Notifications.Domain.Services;
using Notifications.Infraestructure.Persistence;
using Notifications.Infraestructure.Providers.Email;
using Notifications.Infraestructure.Providers.RabbitMq;
using Notifications.Infraestructure.Repositories;
using Notifications.Infraestructure.Services;

namespace Notifications.Infraestructure
{
    public static class InfraestructureExtensions
    {
        public static IServiceCollection ConfigureInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotificationsContext>(options =>
            {
                var version = new MariaDbServerVersion(new Version(10, 6, 11));
                options.UseMySql(GetSetting("NOTIFICATIONS_ConnectionString", configuration), version);
            }, ServiceLifetime.Scoped);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<INotificationService, NotificationService>();

            services
                .ConfigureEmail(configuration)
                .ConfigureRabbitMq(configuration);

            return services;
        }

       

        private static string GetSetting(string setting, IConfiguration configuration) =>
            Environment.GetEnvironmentVariable(setting) ?? configuration[setting];
    }
}
