using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public class OrdersController : ControllerBase
    {
    }
}
