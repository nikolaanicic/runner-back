using Contracts.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace appDostava.Filters.CurrentUser
{
    public class GetCurrentEmailFilter : IAsyncActionFilter
    {

        private IClaimChecker _claimChecker;

        public GetCurrentEmailFilter(IClaimChecker checker)
        {
            _claimChecker = checker;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var authorize = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>();

            if(authorize != null)
            {
                context.HttpContext.Items["currentEmail"] = _claimChecker.GetCurrentEmail(context.HttpContext);
            }

            return next();
        }
    }
}
