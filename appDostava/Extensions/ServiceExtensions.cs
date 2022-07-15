using appDostava.Filters.CurrentUser;
using appDostava.Filters.LogFilter;
using appDostava.Filters.ValidationFilter;
using appDostava.Middleware;
using Contracts.Configuration.Email;
using Contracts.Dtos.Order.Post;
using Contracts.Dtos.Product.Post;
using Contracts.Dtos.User.Patch;
using Contracts.Dtos.User.Post;
using Contracts.Images;
using Contracts.Logger;
using Contracts.Repository;
using Contracts.Security.Claims;
using Contracts.Security.Passwords;
using Contracts.Services;
using Entities.Context;
using Entities.PasswordSecurity;
using Entities.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Services.AdminService;
using Services.ClaimsService;
using Services.EmailService;
using Services.ImageService;
using Services.LoggerService;
using Services.OrderService;
using Services.ProductService;
using Services.UserService;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                    builder.WithOrigins(configuration["AllowedOrigin"])
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                
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



        /// <summary>
        /// Configures high level services injected into controllers or into other services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminManager>()
                .AddScoped<IUserService, UserManager>()
                .AddScoped<IImageService, ImageManager>()
                .AddScoped<IPasswordChecker, PasswordManager>()
                .AddScoped<IPasswordHasher, PasswordManager>()
                .AddScoped<IProductService, ProductManager>()
                .AddScoped<IOrderService, OrderManager>()
                .AddScoped<IClaimChecker, ClaimsManager>()
                .AddScoped<IClaimAdder, ClaimsManager>();
        }



        /// <summary>
        /// This method configures jwt handler and configures google sign in
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["secret_key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if(context.Request.Query.ContainsKey("access_token"))
                        {
                            var token = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/order"))
                            {
                                context.Token = token.ToString().Replace("Bearer ", "");
                            }

                        }

                        return Task.CompletedTask;
                    }
                };
            }).AddGoogle("google",opt=>
            {
                var googleAuth = configuration.GetSection("Authentication:Google");

                opt.ClientId = googleAuth["ClientId"];
                opt.ClientSecret = googleAuth["ClientSecret"];
                opt.SignInScheme = IdentityConstants.ExternalScheme;
                opt.Scope.Add("email");
                opt.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");


            });
        }


        /// <summary>
        /// This method configures emailing server used to contact the deliverers about the status of their accounts
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureEmailServer(this IServiceCollection services,IConfiguration configuration)
        {
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
        }


        /// <summary>
        /// Configures action filters
        /// Logging action filter and validation action filters 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureActionFilters(this IServiceCollection services) =>
            services
            .AddScoped<DtoValidationFilter<PostUserDto>>()
            .AddScoped<DtoValidationFilter<PostProductDto>>()
            .AddScoped<DtoValidationFilter<PostOrderDto>>()
            .AddScoped<DtoValidationFilter<PostUserLogInDto>>()
            .AddScoped<JsonDocumentValidationFilter<UserUpdateDto>>()
            .AddScoped<DtoValidationFilter<RefreshTokenPostDto>>()
            .AddScoped<DtoValidationFilter<CompleteDeliveryDto>>()
            .AddScoped<GetCurrentUserFilter>()
            .AddScoped<GetCurrentEmailFilter>()
            .AddScoped<LogRoute>()
            .AddScoped<IEmailService,EmailManager>();


        public static void ConfigureIIS(this IServiceCollection services) =>
            services.Configure<IISServerOptions>(o => 
            { 
                o.MaxRequestBodySize = int.MaxValue; 
            })
            .Configure<FormOptions>(o=> 
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
            
            });
    }
}
