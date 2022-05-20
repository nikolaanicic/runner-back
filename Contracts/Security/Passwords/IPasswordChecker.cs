
using System.Threading.Tasks;

namespace Contracts.Security.Passwords
{
    public interface IPasswordChecker
    {
        Task<bool> CheckPassword(string plaintextPassword, string hashedPassword);
    }
}
