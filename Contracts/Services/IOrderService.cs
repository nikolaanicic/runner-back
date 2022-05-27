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
        Task<AcceptOrderDto> AcceptOrderAsync(long id, string deliverer);
        Task<IEnumerable<GetOrderDto>> GetCompletedByUsernameAsync(string username);
        Task<IEnumerable<GetOrderDto>> GetActiveAsync();
    }
}
