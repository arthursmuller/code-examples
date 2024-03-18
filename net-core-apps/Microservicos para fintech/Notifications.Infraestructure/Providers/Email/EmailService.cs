using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using Notifications.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Providers.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(EmailConfiguration emailSettings, ILogger<EmailService> logger)
        {
            _config = emailSettings;
            _logger = logger;
        }

        public async Task<bool> Send(string title, List<RecipientDto> recipients, string messageText = null, string templateFileName = null, string template = null, IDictionary<string, string> replacements = null)
        {
            var message = generateTemplate(title, recipients);
            message.Body = generateBody(messageText, templateFileName, template, replacements);
            var messageSent = await sendMessage(message);
            return messageSent;
        }

        private MimeEntity generateBody(string message = null, string templateFileName = null, string template = null, IDictionary<string, string> replacements = null)
        {
            var builder = new BodyBuilder();
            var body = template;

            if(!string.IsNullOrEmpty(templateFileName))
            {
                var path = $"Notifications.Domain/Resources/EmailTemplates/{templateFileName}.html";
                var parentDir = Directory.GetParent(Environment.CurrentDirectory).FullName;
                var filePath = $"{parentDir}/{path}";
                var dir = parentDir.Length > 1 ? filePath : path;

                using (StreamReader reader = new StreamReader(dir))
                {
                    body = reader.ReadToEnd();
                }
            }
           

            if (replacements is not null)
                foreach (var replace in replacements)
                    body = body.Replace("{"+ replace.Key + "}", replace.Value);

            if(!string.IsNullOrEmpty(body))
                builder.HtmlBody = body;

            if(!string.IsNullOrEmpty(message))
                builder.TextBody = message;

            return builder.ToMessageBody();
        }

        private MimeMessage generateTemplate(string title, List<RecipientDto> recipients)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config.FromName, _config.FromEmail));

            if (recipients is not null)
                foreach (var recipient in recipients)
                    message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));

            message.Subject = title;
            
            return message;
        }
        private async Task<bool> sendMessage(MimeMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_config.StmpClient, _config.Port, SecureSocketOptions.StartTlsWhenAvailable);
                    // Note: only needed if the SMTP server requires authentication
                    
                    client.Authenticate(_config.AuthName, _config.AuthPassword);

                    await client.SendAsync(message);

                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return default;
            }
        }
    }
}
