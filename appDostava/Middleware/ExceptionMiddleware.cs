using Contracts.Dtos;
using Contracts.Exceptions;
using Contracts.Logger;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace appDostava.Middleware
{
    /// <summary>
    /// This class is the exception middleware of the application
    /// It should be inserted in the pipeline as soon as possible
    /// This class picks up all of the exceptions thrown in the application
    /// And translates exception objects to the object of the ErrorDetails class
    /// </summary>
    public class ExceptionMiddleware : IMiddleware
    {

        private ILoggerManager _logger;

        public ExceptionMiddleware(ILoggerManager logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// This method tries to invoke the next method in the request pipeline
        /// If one of the methods in the pipeline throws and exception
        /// That exception is handled by the catch branch of the InvokeAsync method code
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch(Exception ex)
            {
                ErrorDetails err = new ErrorDetails { Message = ex.Message };

                httpContext.Response.ContentType = "application/json";
                

                // switching just on the base exception
                // because all of them carry the same data
                // but this way service layer doesn't need to know the http status codes

                switch(ex)
                {
                    case HttpException e:
                        err.StatusCode = e.HttpStatus;
                        break;

                    default:

                        err.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError($"RESPONSE STATUS:{err.StatusCode} RESPONSE MESSAGE:{err.Message}");

                httpContext.Response.StatusCode = err.StatusCode;
                await httpContext.Response.WriteAsync(err.ToString());
            }
        }
    }
}
