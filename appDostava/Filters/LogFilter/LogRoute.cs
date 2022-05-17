using Contracts.Logger;
using Microsoft.AspNetCore.Mvc.Filters;

namespace appDostava.Filters.LogFilter
{
    /// <summary>
    /// Log Action filter
    /// Used to log which action and which controller have been hit by the users request
    /// </summary>
    public class LogRoute : IActionFilter
    {

        private ILoggerManager _logger;


        public LogRoute(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            string controller = (string)context.RouteData.Values["controller"];
            string action = (string)context.RouteData.Values["action"];

            _logger.LogInfo($"HIT ROUTE: {controller}Controller.{action}");

        }
    }
}
