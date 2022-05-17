
using System.Threading.Tasks;

namespace Contracts.Security
{
    public interface IPasswordChecker
    {
        Task<bool> CheckPassword(string plaintextPassword, string hashedPassword);
    }
}
