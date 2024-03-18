namespace ExternalEntities.Domain.Dtos
{
    public class ApplicationUserDto
    {
        public int? Id { get; set; }
        public string Cpf { get; set; }

        public ApplicationUserDto() { }
        public ApplicationUserDto(int? id, string cpf) 
        {
            Id = id;
            Cpf = cpf;
        }
    }
}
