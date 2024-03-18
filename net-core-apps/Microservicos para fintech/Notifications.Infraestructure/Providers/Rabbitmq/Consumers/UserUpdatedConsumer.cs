using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Notifications.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Providers.RabbitMq.Consumers
{
    public class UserUpdatedConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<UserUpdatedConsumer> _logger;
        private readonly IServiceProvider _services;

        public UserUpdatedConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<UserUpdatedConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                            await walletService.Update((int)userId, name, email, cellphone);

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
