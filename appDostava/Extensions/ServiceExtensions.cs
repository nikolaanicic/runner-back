using appDostava.Middleware;
using Contracts.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.LoggerService;

namespace appDostava.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    //builder.WithOrigins(configuration["AllowedOrigin"])
                    //.AllowAnyHeader().AllowAnyMethod();

                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                
                });
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureExceptionService(this IServiceCollection services) => services.AddTransient<IMiddleware,ExceptionMiddleware>();
    }
}
