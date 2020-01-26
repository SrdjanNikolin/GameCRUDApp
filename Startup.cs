using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GameCRUDApp.Domain.Repositories;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameCRUDApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddHttpClient<IGameService, GameAPIService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44314/api/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration["GameAPIServiceKey"]);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => 
                endpoints.MapDefaultControllerRoute()
            );         
        }
    }
}