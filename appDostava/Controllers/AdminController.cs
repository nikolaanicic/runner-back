using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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


    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
    }
}
