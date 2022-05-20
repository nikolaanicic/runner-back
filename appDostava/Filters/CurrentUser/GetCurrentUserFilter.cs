using Contracts.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Filters.CurrentUser
{
    public class GetCurrentUserFilter : IAsyncActionFilter
    {
        private IClaimChecker _claimChecker;

        public GetCurrentUserFilter(IClaimChecker _checker)
        {
            _claimChecker = _checker;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorize = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>();

            if(authorize != null)
            {
                context.HttpContext.Items["currentUser"] = _claimChecker.GetCurrentUser(context.HttpContext); 
            }

            return next();
        }
    }
}
