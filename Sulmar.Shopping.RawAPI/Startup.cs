using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sulmar.Shopping.RawAPI.Middlewares;

namespace Sulmar.Shopping.RawAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFoo, Foo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<MyMiddleware>();

            // dotnet add package Microsoft.AspNetCore.Owin
            app.UseOwin(pipeline => pipeline(environment => OwinHandler));

            app.Use(async (context, next) =>
            {
                Trace.WriteLine($"request {context.Request.Method} {context.Request.Path}");

                await next.Invoke();

                Trace.WriteLine($"response {context.Response.StatusCode}");

            });

            // app.UseMiddleware<LoggerMiddleware>();
            app.UseLogger();

            app.UseMiddleware<RequestAcceptMiddleware>();

            app.Map("/api/customers", CustomersHandler);

            app.Map("/api/sensors", node =>
            {
                // switch
                node.Map("/temp", TempHandler);
                node.Map("/humidity", HumidityHandler);

                // default:
                node.Map(string.Empty, SensorsHandler);
            });

          //  app.Map("/dashboard", DashboardHandler);

            app.Use(async (context, next) =>
            {
                await next.Invoke();
            });


            app.Run(request => request.Response.WriteAsync("Hello World!"));

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }


        // OWIN = Open Web Server Interface for .NET
        // http://owin.org/html/spec/owin-1.0.1.html
        private async Task OwinHandler(IDictionary<string, object> environment)
        {
            string requestMethod = (string) environment["owin.RequestMethod"];
            string requestPath = (string) environment["owin.RequestPath"];

            Stream responseStream = (Stream)environment["owin.ResponseBody"];
            string response = "Hello World!";

            var responseHeaders = (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];
            responseHeaders["Content-Type"] = new string[] { "text/plain" };

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            await responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);

        }

        private void SensorsHandler(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("all sensors"));
        }

        private void HumidityHandler(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("humidity sensors"));
        }

        private void TempHandler(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("temperature sensors"));
        }

        private void DashboardHandler(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("customers"));
        }

        private void CustomersHandler(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("customers"));
        }
    }
}
