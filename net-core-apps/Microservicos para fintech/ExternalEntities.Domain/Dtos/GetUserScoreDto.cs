using System;

namespace ExternalEntities.Domain.Dtos
{
    public class GetScoreDto
    {
        public int Id { get; set; }
        public int? Score { get; set; }
        public decimal? CreditLimit { get; set; }
        public string Cpf { get; set; }
        public GetScoreDto() { }
        public GetScoreDto(int id, int? score, decimal? creditLimit, string cpf)
        {
            Id = id;
            Score = score;
            CreditLimit = creditLimit;
            Cpf = cpf;
        }
    }
    public record UserDto(
        string FirstName,
        string LastName,
        string Email,
        string Cellphone,
        DateTime? BirthDate,
        bool PublicExposed,
        string MonthlyEarnings,
        string Gender,
        string City,
        string Cep,
        string State,
        string Country,
        string Street,
        string Number,
        string Complement,
        string Neighborhood);
}
