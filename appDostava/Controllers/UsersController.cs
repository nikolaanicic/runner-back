using appDostava.Filters.CurrentUser;
using appDostava.Filters.LogFilter;
using appDostava.Filters.ValidationFilter;
using Contracts.Dtos.User.Patch;
using Contracts.Dtos.User.Post;
using Contracts.Exceptions;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
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
    [ServiceFilter(typeof(LogRoute))]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Authorize(Roles = RolesConstants.All)]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        public async Task<IActionResult> GetUserData()
        {
            return Ok(await _userService.GetUser(Convert.ToString(HttpContext.Items["currentUser"])));
        }


        [HttpPost("register")]
        [ServiceFilter(typeof(DtoValidationFilter<PostUserDto>))]
        public async Task<IActionResult> Register([FromForm]PostUserDto user)
        {
            var file = Request.Form.Files[0];
            await _userService.Register(user);
            return NoContent();
        }

        [HttpPatch("update")]
        [Authorize(Roles = RolesConstants.All)]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        [ServiceFilter(typeof(JsonDocumentValidationFilter<UserUpdateDto>))]
        public async Task<IActionResult> UpdateUser([FromBody] JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            await _userService.UpdateUser(patchDocument, Convert.ToString(HttpContext.Items["currentUser"]));
            return NoContent();
        }


        [HttpPost("image")]
        [Authorize(Roles = RolesConstants.All)]
        [ServiceFilter(typeof(GetCurrentUserFilter))]
        public async Task<IActionResult> UpdateProfileImage([FromForm] IFormFile image)
        {
            await _userService.UpdateProfileImage(image, Convert.ToString(HttpContext.Items["currentUser"]));
            return NoContent();
        }
    }
}
