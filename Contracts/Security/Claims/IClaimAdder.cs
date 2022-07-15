using Contracts.Dtos.Login;
using Contracts.Dtos.User.Post;
using Google.Apis.Auth;
using System.Threading.Tasks;

namespace Contracts.Security.Claims
{
    public interface IClaimAdder
    {
        Task<LoginResponseDto> LogIn(PostUserLogInDto login);
        Task<LoginResponseDto> RefreshToken(RefreshTokenPostDto refreshDto);
        Task<LoginResponseDto> CreateGoogleUser(GoogleLoginDto dto);
    }
}
