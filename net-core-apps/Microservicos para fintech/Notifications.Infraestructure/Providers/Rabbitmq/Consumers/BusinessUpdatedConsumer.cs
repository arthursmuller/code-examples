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
    public class BusinessUpdatedConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<BusinessUpdatedConsumer> _logger;
        private readonly IServiceProvider _services;

        public BusinessUpdatedConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<BusinessUpdatedConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                    var businessId = taskMessage.Value<int?>("BusinessId");
                    var ownerIds = taskMessage.Value<string>("OwnerIds");
                    var name = taskMessage.Value<string>("Name");
                    var email = taskMessage.Value<string>("Email");
                    var cellphone = taskMessage.Value<string>("Cellphone");

                    using (var scope = _services.CreateScope())
                    {
                        var walletService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

                        if (string.IsNullOrEmpty(ownerIds) || businessId is null)
                            throw new InvalidOperationException("OwnerIds or businessId Is Missing");

                        if (businessId is not null)
                        {
                            var ids = new List<int> { };
                            foreach (var id in ownerIds.Split(";"))
                                ids.Add(Convert.ToInt32(id));

                            await walletService.Update((int)businessId, name, email, cellphone, ids.ToArray());
                        }

                        _logger.LogInformation(" *************** Success Processing BusinessUpdatedConsumer - businessId: {businessId} ***************  ", businessId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        "BusinessUpdatedConsumer: Message Process fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}",
                        ex.Message,
                        ex.StackTrace,
                        message);

                    _logger.LogError(-1, ex, "BusinessUpdatedConsumer: Message Process fail");

                    throw;
                }
            }
        }
    }
}
