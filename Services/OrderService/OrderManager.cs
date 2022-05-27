using AutoMapper;
using Contracts.Dtos.Order.Get;
using Contracts.Dtos.Order.Post;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public class OrderManager : ModelServiceBase, IOrderService
    {
        public OrderManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }



        public async Task CreateOrder(PostOrderDto newOrder)
        {

            Random r = new Random();
            Order o = new Order();

            IEnumerable<Product> products = await _repository.Products.GetByIdsAsync(newOrder.ProductIds,false);
            var consumer = await _repository.Consumers.GetByUsernameAsync(newOrder.Consumer,false);

            if (consumer == null)
                throw new NotFoundException($"User {newOrder.Consumer} doesn't exist");


            o.ConsumerId = consumer.Id;
            _repository.Orders.Create(o);
                
            o.Produce = (List<Product>)products;

            foreach (var p in products)
                o.TotalPrice += p.Price;

            o.OrderStatus = OrderStatus.WAITING;
            o.DeliveryTimer = (float)r.Next(10, 60);

            await _repository.SaveAsync();
        }

        public async Task AcceptOrderAsync(long id,string deliverer)
        {

            var order = await _repository.Orders.GetByIdAsync(id, true);
            var user = await _repository.Deliverers.GetByUsernameAsync(deliverer, true);

            if (order == null)
                throw new NotFoundException($"Requested order doesn't exist");
            else if (user == null)
                throw new NotFoundException($"Deliverer {deliverer} doesn't exist");
            else if (user.State != ProfileState.APPROVED)
                throw new BadRequestException($"Your account is not yet approved");

            
            order.DelivererId = user.Id;
            user.Busy = true;
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<GetOrderDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetAllAsync(false));
        }

        public async Task<IEnumerable<GetOrderDto>> GetCompletedByUsernameAsync(string username)
        {
            var user = await _repository.Users.GetByUsernameAsync(username, false);

            if (user == null)
                throw new NotFoundException($"User {username} doesn't exist.");

            IEnumerable<Order> orders = null;


            if (user.RoleId == (long)Roles.Consumer)
            {
                return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetCompletedByConsumerAsync(user.Id, false));
            }
            else if (user.RoleId == (long)Roles.Deliverer)
            {
                return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetCompletedByDelivererAsync(user.Id, false));
            }

            throw new BadRequestException($"User{username} is not a Consumer or a Deliverer");

        }

        public async Task<IEnumerable<GetOrderDto>> GetActiveAsync()
        {
            return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetActiveAsync(false));
        }
    }
}
