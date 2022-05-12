using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls for the deliverers requirements
    /// 
    /// 
    /// DELIVERERS REQUIREMENTS:
    /// 
    /// 1. Can see all of the orders that are awaiting to be accepted
    /// 2. Can see previously completed deliveries
    /// 3. Can accept one order at a time
    /// 4. When the deliverer accepts the order, the deliverer gets the time in which he should complete the delivery
    /// </summary>



    [Route("api/courier")]
    [ApiController]
    public class DelivererController : ControllerBase
    {
    }
}
