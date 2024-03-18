using BrDateTimeUtils;
using Notifications.Domain.Abstractions;
using System.Collections.Generic;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public class Recipient : ValueObject
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        private Recipient() { }
        public Recipient(string name, string email, string cellphone)
        {
            Name = name;
            Email = email;
            Cellphone = cellphone;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Email;
            yield return Cellphone;
        }
    }
}
