using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaFincaApi.Models;
using LaFincaApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LaFincaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<LaFincaDatabaseSettings>(
                Configuration.GetSection(nameof(LaFincaDatabaseSettings))
                );

            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<LaFincaDatabaseSettings>>().Value);
            services.AddSingleton<UserService>();
            services.AddSingleton<MenuItemService>();

            services.AddControllers().AddNewtonsoftJson(Options => Options.UseMemberCasing());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=ViewAll}/{id?}");
            });
        }
    }
}
