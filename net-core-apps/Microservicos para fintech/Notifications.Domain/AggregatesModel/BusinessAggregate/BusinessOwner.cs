using Notifications.Domain.AggregatesModel.UserAggregate;

namespace Notifications.Domain.AggregatesModel.BusinessAggregate
{
    public class BusinessOwner
    {
        public ApplicationUser User { get; private set; }
        private int _userId;

        public BusinessOwner() { }
        public BusinessOwner(int userId)
        {
            _userId = userId;
        }
    }
}
