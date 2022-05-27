using Contracts.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Filters.CurrentUser
{

    /// <summary>
    /// Used to get the username of the user that has sent the request
    /// Should be used on actions that require authorization
    /// 
    /// </summary>

    public class GetCurrentUserFilter : IAsyncActionFilter
    {
        private IClaimChecker _claimChecker;

        public GetCurrentUserFilter(IClaimChecker _checker)
        {
            _claimChecker = _checker;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            // Gets the authorize attribute of the action
            // If the action has [Authorize] attribute it will be fetched here
            var authorize = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>();

            if(authorize != null)
            {

                // Gets the users claims from the http context
                // Gets the username that has been embeded in the user claims with the ClaimType.Role

                context.HttpContext.Items["currentUser"] = _claimChecker.GetCurrentUser(context.HttpContext); 
            }

            return next();
        }
    }
}
