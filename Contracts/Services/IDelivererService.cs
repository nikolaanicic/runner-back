using Contracts.Dtos.Order.Get;
using Contracts.Dtos.User.Get;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IDelivererService
    {
        Task<IEnumerable<GetUserDto>> GetDeliverersAsync();
        Task<GetUserDto> GetDelivererByUsername(string username);
    }
}
