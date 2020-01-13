using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCRUDApp.Domain.Repositories;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Repository;
using GameCRUDApp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GameCRUDApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IGameRepository, GameRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc(route => route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
