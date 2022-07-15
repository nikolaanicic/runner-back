using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Contracts.Images
{
    public interface IImageService
    {
        Task<string> Save(IFormFile image, string email);
        Task RemoveImage(string email);
        Task<string> Get(string email);
    }
}
