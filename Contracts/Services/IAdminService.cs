using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IAdminService
    {
        Task<GetAdminDto> GetByUsername(string username);
    }
}
