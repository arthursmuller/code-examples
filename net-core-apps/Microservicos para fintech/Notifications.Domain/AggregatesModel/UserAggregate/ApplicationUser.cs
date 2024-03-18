using Notifications.Domain.Abstractions;

namespace Notifications.Domain.AggregatesModel.UserAggregate
{
    public class ApplicationUser : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        private ApplicationUser() { }
        public ApplicationUser(int id, string name, string email , string cellphone) : base(id) 
        {
            Name = name;
            Email = email;
            Cellphone = cellphone;
        }
    }
}
