using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastucture;
using Infrastucture.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration configure;

        public Startup(IConfiguration configuration)
        {
            this.configure = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configure.GetConnectionString("MyPortfolioBD"));
            });

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // to spasify how the route should likes like and if there is no route so the default is Home/Index
                endpoints.MapControllerRoute(
                    "defaultRoute",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
