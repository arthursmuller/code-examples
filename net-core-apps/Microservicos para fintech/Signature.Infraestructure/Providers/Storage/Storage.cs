using System.Threading.Tasks;
using StorageProvider;
using StorageProvider.Dtos;

namespace Signature.Infraestructure.Providers.Storage
{
    public class StorageProvider : IStorage
    {
        private readonly StorageConfiguration _config;
        public readonly IStorageProviderService _storageService;

        public StorageProvider(StorageConfiguration config, IStorageProviderService storageService)
        {
            _config = config;
            _storageService = storageService;
        }

        public async Task<string> UploadFinancingProductContractCertificates(string base64, string fileName, int? userId = null)
            => await _storageService.Upload(
                base64String: base64,
                containerName: _config.ContainerNameFinancingProductContractCertificates,
                fileName: fileName,
                userId: userId,
                useQueue: true,
                fileExtension: "pdf",
                fileMimeType: "application/pdf");
        public async Task<string> UploadUserPicture(string base64, string fileName, int? userId = null)
            => await _storageService.Upload(
                base64String: base64,
                containerName: _config.ContainerNameUserSignaturePictures,
                fileName: fileName,
                userId: userId,
                useQueue: true,
                fileExtension: "jpg",
                fileMimeType: "image/jpg");
        public async Task<string> UploadUserSignature(string base64, string fileName, int? userId = null)
            => await _storageService.Upload(
                base64String: base64,
                containerName: _config.ContainerNameUserSignatureDrawings,
                fileName: fileName,
                userId: userId,
                useQueue: true,
                fileExtension: "jpg",
                fileMimeType: "image/jpg");

        public async Task<DownloadDto> Get(string url) => await _storageService.GetFull(url);
        public async Task<string> Update(string url, string base64)
            => await _storageService.Update(
                endpoint: url, 
                base64String: base64, 
                useQueue: true);

        public async Task Delete(string url) => await _storageService.Delete(endpoint: url);
    }
}