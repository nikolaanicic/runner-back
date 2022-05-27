using Contracts.Dtos.Order.Get;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IAdminService
    {
        Task ApproveAccountAsync(string deliverer);
        Task DisapproveAccountAsync(string deliverer);
    }
}
