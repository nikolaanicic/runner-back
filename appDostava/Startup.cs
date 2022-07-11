using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using appDostava.Extensions;
using NLog;
using System.IO;
using Contracts.MappingProfile;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Contracts.Logger;
using appDostava.HubConfig;

namespace appDostava
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // configures NLog with the configuration given in the nlog.config file
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }
        public ILoggerManager logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureExceptionService();
            services.ConfigureCors(Configuration);

            services.ConfigureIIS();
            services.ConfigureLoggerService();
            services.ConfigureDatabase(Configuration);
            services.ConfigureRepositoryManager();

            services.ConfigureActionFilters();

            services.AddAutoMapper(op => op.AddProfile(new MappingProfile()));
            services.ConfigureControllerServices();
            services.ConfigureJWT(Configuration);

            services.ConfigureEmailServer(this.Configuration);
            services.AddSignalR();
            services.AddControllers(config=>config.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "appDostava", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "appDostava v1"));
            }


            app.UseCors("DefaultPolicy");
            app.ConfigureStaticFilesServing();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureExceptionMiddleware();


            app.Use(next => context =>
              {
                  context.Request.EnableBuffering();
                  return next(context);
              });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OrderHub>("/order");
            });

        }
    }
}
