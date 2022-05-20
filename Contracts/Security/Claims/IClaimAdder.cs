using Contracts.Dtos.User.Post;
using System.Threading.Tasks;

namespace Contracts.Security.Claims
{
    public interface IClaimAdder
    {
        Task<string> LogIn(PostUserLogInDto login);
    }
}
