using Contracts.Dtos;

namespace Contracts.Auth
{
    public interface IAuthService
    {
        string Login(LoginUserDto toLogin);
        void Register(RegisterUserDto toRegister);
    }
}
