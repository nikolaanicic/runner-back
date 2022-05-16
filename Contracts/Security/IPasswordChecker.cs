
namespace Contracts.Security
{
    public interface IPasswordChecker
    {
        bool CheckPassword(string plaintextPassword, string hashedPassword);
    }
}
