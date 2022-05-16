using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using System.Collections.Generic;

namespace Contracts.Services
{
    public interface IUserService
    {

        IEnumerable<GetUserDto> GetAllUsers();
    }
}
