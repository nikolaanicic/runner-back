using Contracts.Dtos.User.Patch;
using Contracts.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
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


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var receivedModel = context.ActionArguments.SingleOrDefault(p => p.Value is T).Value;


            string message = string.Empty;
            if(receivedModel == null)
            {
                message = $"Received object of is null";
                context.Result = new BadRequestObjectResult(message);
            }
            else if(!context.ModelState.IsValid)
            {
                message = context.ModelState.ToString();
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

            if (message != string.Empty)
                _logger.LogError(message);
            else
                await next();
        }
    }
}
