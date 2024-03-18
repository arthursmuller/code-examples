using Notifications.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Providers.Email
{
    public interface IEmailService
    {
        Task<bool> Send(
            string title,
            List<RecipientDto> recipients, 
            string messageText = null, 
            string templateFileName = null, 
            string template = null, 
            IDictionary<string, string> replacements = null);
    }
}
