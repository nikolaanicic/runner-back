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
using System.Linq;
using System.Threading.Tasks;

namespace Services.OrderService
{

    /// <summary>
    /// This service class provides funcionalities to work with orders in the application
    /// It provides a way to view, create and accept orders
    /// </summary>
    public class OrderManager : ModelServiceBase, IOrderService
    {
        public OrderManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }


        /// <summary>
        /// This method creates the order 
        /// Gets the wanted products from the db if they exist
        /// Sets the consumer id on the order
        /// Connects the wanted products to the order
        /// Calculates the total price of the order
        /// Generates a random time until delivery in range of 10-60 minutes
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public async Task CreateOrder(PostOrderDto newOrder)
        {

            Random r = new Random();
            Order o = new Order();

            IEnumerable<Product> products = await _repository.Products.GetByIdsAsync(newOrder.ProductIds,false);
            var consumer = await _repository.Consumers.GetByUsernameAsync(newOrder.Consumer,false);

            if (consumer == null)
                throw new NotFoundException($"User {newOrder.Consumer} doesn't exist");
            else if (products.Count() == 0)
                throw new BadRequestException($"Please specify available products");

            o.ConsumerId = consumer.Id;
            _repository.Orders.Create(o);
                
            o.Produce = (List<Product>)products;

            foreach (var p in products)
                o.TotalPrice += p.Price;

            o.OrderStatus = OrderStatus.WAITING;
            o.DeliveryTimer = (float)r.Next(10, 60);

            await _repository.SaveAsync();
        }


        /// <summary>
        /// This method accepts the order for the deliverer
        /// Checks if the wanted order exists
        /// Checks if the deliverer exists and if the state of the deliveres profile is APPROVED and if the deliverer already has an order
        /// Sets deliverers id in the order
        /// Sets the deliverers busy field to true
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deliverer"></param>
        /// <returns></returns>
        public async Task<AcceptOrderDto> AcceptOrderAsync(long id, string deliverer)
        {

            var order = await _repository.Orders.GetByIdAsync(id, true);
            var user = await _repository.Deliverers.GetByUsernameAsync(deliverer, true);

            if (order == null)
                throw new NotFoundException($"Requested order doesn't exist");
            else if (user == null)
                throw new NotFoundException($"Deliverer {deliverer} doesn't exist");
            else if (user.State != ProfileState.APPROVED)
                throw new BadRequestException($"Your account is not yet approved");
            else if (user.Busy)
                throw new BadRequestException($"You haven't completed your active delivery");

            
            order.DelivererId = user.Id;
            user.Busy = true;
            await _repository.SaveAsync();

            return new AcceptOrderDto { Timer = order.DeliveryTimer };
        }


        /// <summary>
        /// This method gets all of the orders with all of the children properties
        /// Gets connected consumer and deliverer if there is one
        /// Gets the connected products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GetOrderDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetAllAsync(false));
        }


        /// <summary>
        /// This method gets completed orders by users username
        /// Gets the user by username 
        /// Checks which role is the user in (should always be consumer or deliverer)
        /// And then gets the orders from the database based on the role and user id
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetOrderDto>> GetCompletedByUsernameAsync(string username)
        {
            var user = await _repository.Users.GetByUsernameAsync(username, false);

            if (user == null)
                throw new NotFoundException($"User {username} doesn't exist.");


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



        /// <summary>
        /// This method gets the currently active orders i.e. the orders that do not have a deliverer attached to them
        /// And thier state is WAITING
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GetOrderDto>> GetActiveAsync()
        {
            return _mapper.Map<IEnumerable<GetOrderDto>>(await _repository.Orders.GetActiveAsync(false));
        }
    }
}
