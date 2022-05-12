using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controlle should provide api calls for consumers requirements
    /// 
    /// 
    /// CONSUMERS REQUIREMENTS:
    /// 
    /// 1. Sees all items
    /// 2. Can make an order, order contains one or multiple items
    /// 3. Can see order history
    /// 4. Once order is accepted by some deliverer, consumer gets the time until delivery
    /// </summary>



    [Route("api/consumer")]
    [ApiController]
    public class ConsumersController : ControllerBase
    {
    }
}
