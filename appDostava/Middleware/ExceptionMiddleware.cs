using Contracts.Dtos;
using Contracts.Exceptions;
using Contracts.Logger;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace appDostava.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {

        private ILoggerManager _logger;

        public ExceptionMiddleware(ILoggerManager logger)
        {
            _logger = logger;
        }

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
                
                switch(ex)
                {
                    case UnauthorizedException e:
                        err.StatusCode = e.HttpStatus;
                        break;

                    default:

                        err.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError(err.ToString());

                httpContext.Response.StatusCode = err.StatusCode;
                await httpContext.Response.WriteAsync(err.ToString());
            }
        }
    }
}
