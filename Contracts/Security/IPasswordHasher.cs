
namespace Contracts.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
