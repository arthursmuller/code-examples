using Notifications.Domain.Abstractions;
using System.Collections.Generic;

namespace Notifications.Domain.AggregatesModel.BusinessAggregate
{
    public class Business : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public IReadOnlyCollection<BusinessOwner> Owners => _owners;
        private List<BusinessOwner> _owners;

        private Business() { }
        public Business(int id, string name, string email, string cellphone, int[] userIds) : base(id)
        {
            Name = name;
            Email = email;
            Cellphone = cellphone;
            UpdateOwners(userIds);
        }

        public void UpdateOwners(int[] userIds)
        {
            var owners = new List<BusinessOwner>();

            foreach (var userId in userIds)
                owners.Add(new BusinessOwner(userId));

            _owners = owners;
        }
    }
}
