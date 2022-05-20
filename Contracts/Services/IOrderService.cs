using Contracts.Dtos.Order.Get;
using Contracts.Dtos.Order.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IOrderService
    {
        Task CreateOrder(PostOrderDto newOrder);
        Task<IEnumerable<GetOrderDto>> GetAllAsync();
        Task AcceptOrderAsync(long id,string deliverer);
    }
}
