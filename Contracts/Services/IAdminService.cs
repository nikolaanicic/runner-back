using Contracts.Dtos.Order.Get;
using Contracts.Dtos.User.Get;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IAdminService
    {
        Task ApproveAccountAsync(string deliverer);
        Task DisapproveAccountAsync(string deliverer);
        Task<IEnumerable<GetDelivererDto>> GetPendingDeliverers();
    }
}
