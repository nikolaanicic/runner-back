using appDostava.Middleware;
using Contracts.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace appDostava.Extensions
{
    public static class AppExtensions
    {

        /// <summary>
        /// This method configures the application for using services that implement
        /// IMiddleware interface
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<IMiddleware>();
        }



        /// <summary>
        /// This method configures the backend server(iis or kestrel) for static file serving
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureStaticFilesServing(this IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions() 
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
                RequestPath = new PathString("/Resources")
            });
        }
    }
}
