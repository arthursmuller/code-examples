using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Notifications.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Providers.RabbitMq.Consumers
{
    public class UserCreatedConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<UserCreatedConsumer> _logger;
        private readonly IServiceProvider _services;

        public UserCreatedConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<UserCreatedConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                    var name = taskMessage.Value<string>("FirstName");
                    var email = taskMessage.Value<string>("Email");
                    var cellphone = taskMessage.Value<string>("Cellphone");

                    using (var scope = _services.CreateScope())
                    {
                        var walletService = scope.ServiceProvider.GetRequiredService<IUserService>();

                        if (userId is not null)
                            await walletService.Add((int)userId, name, email, cellphone);

                        _logger.LogInformation(" *************** Success Processing UserCreatedConsumer - userId: {userId} ***************  ", userId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        "UserCreatedConsumer: Message Process fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}",
                        ex.Message,
                        ex.StackTrace,
                        message);

                    _logger.LogError(-1, ex, "UserCreatedConsumer: Message Process fail");

                    throw;
                }
            }
        }
    }
}
