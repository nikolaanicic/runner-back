﻿using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls for the admins requirements
    /// 
    /// ADMINS REQUIREMENTS:
    /// 
    /// 1. Sees all of the apps users (Consumers, Deliverers and Admins)
    /// 2. Sees all orders (previous and currently active)
    /// 3. Sees all deliverers
    /// 4. Can at any time disallow any of the deliverers on the app
    /// 5. Reviews requests of the user to become a deliverer and allows or dissallows those requests
    /// </summary>


    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;


        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpPatch("approve/{username}")]
        public async Task<IActionResult> ApproveDeliverer(string username)
        {
            await _adminService.ApproveAccountAsync(username);
            return NoContent();

        }


        [HttpPatch("disapprove/{username}")]
        public async Task<IActionResult> DisapproveDeliverer(string username)
        {
            await _adminService.DisapproveAccountAsync(username);
            return NoContent();
        }
    }
}
