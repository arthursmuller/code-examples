namespace ExternalEntities.Domain.Dtos
{
    public class BusinessDto
    {
        public int? Id { get; set; }
        public string Cnpj { get; set; }

        private BusinessDto() { }
        public BusinessDto(int? id, string cnpj) 
        {
            Id = id;
            Cnpj = cnpj;
        }
    }
}
