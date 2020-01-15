using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GameCRUDApp.Domain.Repositories;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Repository;
using GameCRUDApp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameCRUDApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddHttpClient<IGameService, GameAPIService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44314/api/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1Nzg2NjkwMTEsImV4cCI6MTU3OTI3MzgxMSwiaWF0IjoxNTc4NjY5MDExfQ.vOQ_5iM91F2whI_qxAV2lMp49r2w_2BCqL00uZcIMGs");
            });
            //services.AddScoped<IGameService, GameAPIService>();
            services.AddSingleton<IGameRepository, GameRepository>();
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