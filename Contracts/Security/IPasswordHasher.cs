
using System.Threading.Tasks;

namespace Contracts.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        Task<string> HashPasswordAsync(string password);
    }
}
