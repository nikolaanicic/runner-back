using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Dtos.User.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>> GetAllUsers();
        Task Register(PostUserDto newUser);
    }
}
