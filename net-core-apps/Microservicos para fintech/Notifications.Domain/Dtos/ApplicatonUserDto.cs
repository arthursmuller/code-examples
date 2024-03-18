namespace Notifications.Domain.Dtos
{
    public class ApplicationUserDto
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }

        private ApplicationUserDto() { }
        public ApplicationUserDto(int? id, string email, string cellphone)  {
            Id = id;
            Email = email;
            Cellphone = cellphone;
        }
    }
}
