using Notifications.Domain.AggregatesModel.UserAggregate;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public class UserNotification : NotificationBase
    {
        public ApplicationUser UserOwner { get; set; }
        private int? _userOwnerId;
        public UserNotification() { }
        public UserNotification(int userId)
        {
            _userOwnerId = userId;
        }
    }
}
