using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Contracts.Images
{
    public interface IImageService
    {
        Task<string> Save(IFormFile image, string username);
        Task<string> Get(string username);
    }
}
