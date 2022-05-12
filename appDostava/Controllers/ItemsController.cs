using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls that work with items
    /// 
    /// 
    /// 1. Items are added by the admins of the app
    /// 2. Items can be seen by the consumers and admins
    /// </summary>


    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
    }
}
