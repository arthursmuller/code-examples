namespace Notifications.Domain.Dtos
{
    public class BusinessDto
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }

        private BusinessDto() { }
        public BusinessDto(int? id, string email, string cellphone) 
        {
            Id = id;
            Email = email;
            Cellphone = cellphone;
        }
    }
}
