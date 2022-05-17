using appDostava.Filters.LogFilter;
using appDostava.Filters.ValidationFilter;
using appDostava.Middleware;
using Contracts.Dtos.User.Post;
using Contracts.Images;
using Contracts.Logger;
using Contracts.Repository;
using Contracts.Security;
using Contracts.Services;
using Entities.Context;
using Entities.PasswordSecurity;
using Entities.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.AdminService;
using Services.ImageService;
using Services.LoggerService;
using Services.UserService;

namespace appDostava.Extensions
{
    public static class ServiceExtensions
    {


        /// <summary>
        /// Configures applications cors policy
        /// Should only allow frontend app requests
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
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

        /// <summary>
        /// Configures custom logger derived from NLog
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();


        /// <summary>
        ///  Configures custom exception handling middleware
        /// </summary>
        /// <param name="services"></param>

        public static void ConfigureExceptionService(this IServiceCollection services) => services.AddTransient<IMiddleware,ExceptionMiddleware>();


        /// <summary>
        /// Configures database context to use the sql server provider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>

        public static void ConfigureDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(op => op.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }


        /// <summary>
        /// Adds repo manager as a scoped service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryManager(this IServiceCollection services) => 
            services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminManager>()
                .AddScoped<IUserService,UserManager>()
                .AddScoped<IImageService,ImageManager>()
                .AddScoped<IPasswordChecker,PasswordManager>()
                .AddScoped<IPasswordHasher,PasswordManager>();
        }

        public static void ConfigureActionFilters(this IServiceCollection services) =>
            services.AddScoped<DtoValidationFilter<PostUserDto>>()
            .AddScoped<LogRoute>();
    }
}
