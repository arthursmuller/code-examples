using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Notifications.Infraestructure.Providers.Email
{
    public static class EmailExtensions
    {
        public static IServiceCollection ConfigureEmail(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureSettings(services, configuration);
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }

        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = new EmailConfiguration()
            {
                FromName = GetSetting("EMAIL_FromName", configuration),
                FromEmail = GetSetting("EMAIL_FromEmail", configuration),
                AuthName = GetSetting("EMAIL_AuthName", configuration),
                AuthPassword = GetSetting("EMAIL_AuthPassword", configuration),
                StmpClient = GetSetting("EMAIL_StmpClient", configuration),
                Port = Int32.Parse(GetSetting("EMAIL_Port", configuration)),
            };

            services.AddSingleton(emailSettings);

            return services;
        }

        private static string GetSetting(string setting, IConfiguration configuration) =>
            Environment.GetEnvironmentVariable(setting) ?? configuration[setting];
    }
}