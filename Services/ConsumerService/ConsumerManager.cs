using AutoMapper;
using Contracts.Dtos.Order.Get;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Repository;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConsumerService
{
    public class ConsumerManager : ModelServiceBase, IConsumerService
    {
        public ConsumerManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }

        public async Task<IEnumerable<GetOrderDto>> GetCompletedOrdersByUsernameAsync(string consumer)
        {

            var user = await _repository.Consumers.GetByUsernameAsync(consumer, false);

            if (user == null)
                throw new NotFoundException($"Consumer {consumer} doesn't exist");

            return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetCompletedByConsumerAsync(user.Id, false));
        }
    }
}
