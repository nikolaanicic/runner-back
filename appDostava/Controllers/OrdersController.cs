﻿using appDostava.Filters.CurrentUser;
using appDostava.Filters.LogFilter;
using appDostava.Filters.ValidationFilter;
using appDostava.HubConfig;
using Contracts.Dtos.Order.Post;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls that work with the orders
    /// 
    /// 
    /// 1. Orders can be created by the consumers of the app
    /// 2. Orders can be accepted by the deliverers of the app
    /// 3. Previous orders can be seen by the consumers and the deliverers of the app
    /// 4. Currently active orders can be seen by the consumers of the app
    /// 5. Admins can see all of the orders 
    /// </summary>



    [Route("api/orders")]
    [ApiController]
    [ServiceFilter(typeof(LogRoute))]
    public class OrdersController : ControllerBase
    {

        private IOrderService _orderService;
        private IHubContext<OrderHub> _hub;

        public OrdersController(IOrderService orderService , IHubContext<OrderHub> hub)
        {
            _orderService = orderService;
            _hub = hub;
        }


        [HttpPost("deliver")]
        [Authorize(Roles = RolesConstants.Deliverer)]
        [ServiceFilter(typeof(DtoValidationFilter<CompleteDeliveryDto>))]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        [ServiceFilter(typeof(GetCurrentEmailFilter))]
        public async Task<IActionResult> CompleteDelivery([FromBody]CompleteDeliveryDto dto)
        {
            await _orderService.Deliver(dto, Convert.ToString(HttpContext.Items["currentEmail"]));
            return NoContent();
        }


        [HttpPost("create")]
        [Authorize(Roles = RolesConstants.Consumer)]
        [ServiceFilter(typeof(DtoValidationFilter<PostOrderDto>))]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        [ServiceFilter(typeof(GetCurrentEmailFilter))]

        public async Task<IActionResult> CreateOrder([FromBody] PostOrderDto newOrder)
        {
            await _orderService.CreateOrder(newOrder, Convert.ToString(HttpContext.Items["currentEmail"]));
            return NoContent();
        }


        [HttpGet("completed")]
        [Authorize(Roles = RolesConstants.ConsumerDeliverer)]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        [ServiceFilter(typeof(GetCurrentEmailFilter))]

        public async Task<IActionResult> GetCompletedOrders()
        {
            return Ok(await _orderService.GetCompletedByUsernameAsync(Convert.ToString(HttpContext.Items["currentEmail"])));
        }

        [HttpPatch("accept/{id}")]
        [Authorize(Roles = RolesConstants.Deliverer)]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        [ServiceFilter(typeof(GetCurrentEmailFilter))]

        public async Task<IActionResult> AcceptOrder(long id)
        {
            var res = await _orderService.AcceptOrderAsync(id, Convert.ToString(HttpContext.Items["currentEmail"]));
            await _hub.Clients.User(res.Item2).SendAsync("pushTimer", new {Timer = res.Item1.Timer, Data = res.Item3 });
            return Ok(res.Item1);
        }


        [HttpGet("all")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllAsync());
        }

        [HttpGet("active")]
        [Authorize(Roles = RolesConstants.Deliverer)]
        public async Task<IActionResult> GetActiveOrders()
        {
            return Ok(await _orderService.GetActiveAsync());
        }
    }
}
