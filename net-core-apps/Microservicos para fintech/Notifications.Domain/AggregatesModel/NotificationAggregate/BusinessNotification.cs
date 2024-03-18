using Notifications.Domain.AggregatesModel.BusinessAggregate;
using System.Collections.Generic;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public class BusinessNotification : NotificationBase
    {
        public Business BusinessOwner { get; set; }
        private int? _businessOwnerId;
        public BusinessNotification() { }
        public BusinessNotification(int businessId)
        {
            _businessOwnerId = businessId;
        }
    }
}
