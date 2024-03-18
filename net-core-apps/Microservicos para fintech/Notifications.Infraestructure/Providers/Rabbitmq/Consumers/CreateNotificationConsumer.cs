using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Notifications.Domain.Dtos;
using Notifications.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Providers.RabbitMq.Consumers
{
    public class CreateNotificationConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<CreateNotificationConsumer> _logger;
        private readonly IServiceProvider _services;

        public CreateNotificationConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<CreateNotificationConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                    var userId = taskMessage.Value<int? >("UserId");
                    var msg = taskMessage.Value<string>("Message");
                    var description = taskMessage.Value<string>("Description");
                    var title = taskMessage.Value<string>("Title");
                    var templateFileName = taskMessage.Value<string>("TemplateFileName");
                    var template = taskMessage.Value<string>("Template");
                    var channelIds = taskMessage.SelectToken("ChannelIds").ToObject<List<int>>();
                    var replacements = taskMessage.SelectToken("Replacements").ToObject<IDictionary<string, string>>();
                    var recipients = taskMessage.SelectToken("Recipients").ToObject<List<RecipientDto>>();
                    
                    using (var scope = _services.CreateScope())
                    {
                        var storageService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                        var res = await storageService.Add(businessId, userId, msg, description, title, channelIds, recipients, replacements, templateFileName, template);
                    }

                    _logger.LogInformation(" *************** Success Processing CreateNotificationConsumer - " +
                        "businessId: {businessId} userId : {userId} ***************  ", businessId, userId);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        "CreateNotificationConsumer: Message Process fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}",
                        ex.Message,
                        ex.StackTrace,
                        message);

                    _logger.LogError(-1, ex, "CreateNotificationConsumer: Message Process fail");

                    throw;
                }
            }
        }
    }
}
