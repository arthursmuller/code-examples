using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Dtos;
using System.Threading.Tasks;

namespace ExternalEntities.Domain.Services
{
    public interface IUserService
    {
        Task<GetScoreDto> GetScore(string cpf);
        Task<UserDto> Get(string cpf);
        Task<GetScoreDto> GetScore(int id);
        Task<GetScoreDto> SimulateScore(string cpf);
        Task<GetScoreDto> SimulateScore(int id);
        Task Add(int id, string cpf);
        Task Update(int id, string cpf);
    }
}
