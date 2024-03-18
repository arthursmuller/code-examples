using System.Collections.Generic;

namespace Notifications.Infraestructure.Providers.Email
{
    public class EmailConfiguration
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string AuthName { get; set; }
        public string AuthPassword { get; set; }
        public string StmpClient { get; set; }
        public int Port { get; set; }
    }
}
