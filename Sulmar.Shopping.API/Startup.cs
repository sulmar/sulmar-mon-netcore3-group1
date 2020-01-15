using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.Models.Validators;
using Sulmar.Shopping.Domain.Services;
using Sulmar.Shopping.Infrastructure;
using Sulmar.Shopping.Infrastructure.Fakers;

namespace Sulmar.Shopping.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public Startup(IHostEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange:true);

        //    Configuration = builder.Build();
        //}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.Configure<RequestLocalizationOptions>(options =>
             options.DefaultRequestCulture = new RequestCulture("fr-FR"));
         

            services.AddScoped<ICustomerRepository, FakeCustomerRepository>();
            services.AddScoped<CustomerFaker>();


            // IOptions<T>
            services.Configure<FakeCustomerRepositoryOptions>(Configuration.GetSection("FakeCustomerRepositoryOptions"));

            // surowe
            //var customersOptions = new FakeCustomerRepositoryOptions();
            //Configuration.GetSection("FakeCustomerRepositoryOptions").Bind(customersOptions);
            //services.AddSingleton(customersOptions);

            // dotnet add package FluentValidation.AspNetCore
            services.AddControllers()

                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CustomerValidator>());

        }

        //public void ConfigureTesting(IApplicationBuilder app, IWebHostEnvironment env)
        //{

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            int customersQty = int.Parse(Configuration["Shopping:CustomersQty"]);

            string connectionString = Configuration.GetConnectionString("ShoppingConnection");

            if (env.EnvironmentName == "Testing")
            {

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

        

            app.UseRouting();

            app.UseAuthorization();

            app.UseRequestLocalization();
            // app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
