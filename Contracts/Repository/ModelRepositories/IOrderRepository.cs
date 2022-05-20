using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories
{
    public interface IOrderRepository : ICrudBase<Order>
    {
        Task<Order> GetByIdAsync(long id,bool trackChanges);
    }
}
