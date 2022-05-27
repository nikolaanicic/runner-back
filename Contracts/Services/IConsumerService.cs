using Contracts.Dtos.Order.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IConsumerService
    {
        Task<IEnumerable<GetOrderDto>> GetCompletedOrdersByUsernameAsync(string consumer);
    }
}
