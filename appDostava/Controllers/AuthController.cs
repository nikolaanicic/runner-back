using appDostava.Filters.LogFilter;
using appDostava.Filters.ValidationFilter;
using Contracts.Dtos.User.Post;
using Contracts.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    [ServiceFilter(typeof(LogRoute))]
    public class AuthController : ControllerBase
    {
        private IClaimAdder _tokenGenerator;

        public AuthController(IClaimAdder claimsAdder)
        {
            _tokenGenerator = claimsAdder;
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(DtoValidationFilter<PostUserLogInDto>))]
        public async Task<IActionResult> LogIn([FromBody] PostUserLogInDto login)
        {
            return Ok(await _tokenGenerator.LogIn(login));
        }


        [HttpPost("refresh")]
        [ServiceFilter(typeof(DtoValidationFilter<RefreshTokenPostDto>))]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenPostDto dto)
        {
            return Ok(await _tokenGenerator.RefreshToken(dto));
        }
    }
}
