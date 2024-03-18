using StorageProvider.Dtos;
using System.Threading.Tasks;

namespace Signature.Infraestructure.Providers.Storage
{
    public interface IStorage
    {
        Task<string> UploadFinancingProductContractCertificates(string base64, string fileName, int? userId = null);
        Task<string> UploadUserPicture(string base64, string fileName, int? userId = null);
        Task<string> UploadUserSignature(string base64, string fileName, int? userId = null);
        Task<string> Update(string documentUrl, string base64);
        Task<DownloadDto> Get(string url);
        Task Delete(string url);
    }
}

