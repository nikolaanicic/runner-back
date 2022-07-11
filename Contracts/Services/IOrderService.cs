using Contracts.Dtos.Order.Get;
using Contracts.Dtos.Order.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IOrderService
    {
        Task CreateOrder(PostOrderDto newOrder,string consumer);
        Task<IEnumerable<GetOrderDto>> GetAllAsync();
        Task<Tuple<AcceptOrderDto,string,GetOrderDto>> AcceptOrderAsync(long id, string deliverer);
        Task<IEnumerable<GetOrderDto>> GetCompletedByUsernameAsync(string username);
        Task<IEnumerable<GetOrderDto>> GetActiveAsync();
        Task Deliver(CompleteDeliveryDto dto, string deliverer);
    }
}
