using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Notifications.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Providers.RabbitMq.Consumers
{
    public class UserUpdatedEmailConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<UserUpdatedEmailConsumer> _logger;
        private readonly IServiceProvider _services;

        public UserUpdatedEmailConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<UserUpdatedEmailConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
        {
            _logger = logger;
            _services = services;
            base.CreateConnection();
            base.RegisterQueue();
        }

        public override async Task ProcessMessageAsync(string message)
        {
            var taskMessage = JToken.Parse(message);

            if (taskMessage != null)
            {
                try
                {
                    var userId = taskMessage.Value<int?>("UserId");
                    var email = taskMessage.Value<string>("Email");

                    using (var scope = _services.CreateScope())
                    {
                        var walletService = scope.ServiceProvider.GetRequiredService<IUserService>();

                        if (userId is not null)
                            await walletService.Update((int)userId, email: email);

                        _logger.LogInformation(" *************** Success Processing UserUpdatedConsuer - userId: {userId} ***************  ", userId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        "UserUpdatedConsuer: Message Process fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}",
                        ex.Message,
                        ex.StackTrace,
                        message);

                    _logger.LogError(-1, ex, "UserUpdatedConsuer: Message Process fail");

                    throw;
                }
            }
        }
    }
}
