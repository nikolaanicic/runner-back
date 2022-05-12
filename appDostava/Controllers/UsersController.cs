using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls that work with user profile data
    /// 
    /// 
    /// 1. Users can see their profile data
    /// 2. Admin can see other users profiles
    /// 3. Users can update their profiles
    /// </summary>


    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
    }
}
