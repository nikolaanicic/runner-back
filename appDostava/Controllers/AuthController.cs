using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls for the authentication and authorization of the application
    /// 
    /// 
    /// AUTH REQUIREMENTS:
    /// 
    /// 1. Users register with dataset from the specification (username, password, name,last name etc...)
    /// 2. Users login on the app with the next dataset (username, password)
    /// 3. Users can update their profiles
    /// </summary>


    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    }
}
