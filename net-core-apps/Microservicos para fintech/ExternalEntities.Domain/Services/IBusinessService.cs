using ExternalEntities.Domain.Dtos;
using System.Threading.Tasks;

namespace ExternalEntities.Domain.Services
{
    public interface IBusinessService
    {
        Task Add(int id, string cnpj, int[] userIds);
        Task Update(int id, string cnpj, int[] userIds);
        Task<AnalysisResDto> PayiedAnalysis(PayiedAnalysisDto req);
    }
}
