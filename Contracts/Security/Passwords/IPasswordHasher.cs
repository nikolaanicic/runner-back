
using System.Threading.Tasks;

namespace Contracts.Security.Passwords

{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        Task<string> HashPasswordAsync(string password);
    }
}
