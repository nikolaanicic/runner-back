using Contracts.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace appDostava.Filters.ValidationFilter
{

    /// <summary>
    /// This class validates dto models received from frontend
    /// T must be type of the dto class you want to validate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DtoValidationFilter<T> : IAsyncActionFilter
        where T:class
    {

        private ILoggerManager _logger;

        public DtoValidationFilter(ILoggerManager logger)
        {
            _logger = logger;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            var receivedModel = context.ActionArguments.SingleOrDefault(p => p.Value is T).Value;

            string message = string.Empty;
            if(receivedModel == null)
            {
                message = $"Received object of type {nameof(T)} is null";
                context.Result = new BadRequestObjectResult(message);
            }
            else if(!context.ModelState.IsValid)
            {
                message = context.ModelState.ToString();
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

            if (message != string.Empty)
                _logger.LogError(message);


            return next();
        }
    }
}
