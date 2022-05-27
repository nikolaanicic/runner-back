using Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories
{
    public interface IOrderRepository : ICrudBase<Order>
    {
        Task<Order> GetByIdAsync(long id,bool trackChanges);
        Task<IEnumerable<Order>> GetActiveAsync(bool trackChanges);
        Task<IEnumerable<Order>> GetCompletedByDelivererAsync(long id, bool trackChanges);
        Task<IEnumerable<Order>> GetCompletedByConsumerAsync(long id, bool trackChanges);
    }
}
