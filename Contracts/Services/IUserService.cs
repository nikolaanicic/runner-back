using Contracts.Dtos.User.Get;
using Contracts.Dtos.User.Patch;
using Contracts.Dtos.User.Post;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>> GetAllUsers();
        Task Register(PostUserDto newUser);
        Task UpdateUser(JsonPatchDocument<UserUpdateDto> patchDocument, string username);
    }
}
