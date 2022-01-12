using Microsoft.AspNetCore.Http;

namespace ReactMovieApi.Services
{
    public interface IFileStorageService
    {
        Task DeleteFile(string fileRoute, string containerName);
        Task<String> SaveFile(string containerName, IFormFile file);
        Task<String> EditFile(string containerName, IFormFile file, string fileRoute);
    }
}
