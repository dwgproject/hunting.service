using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuntingHelperWebService.ApplicationContext;
using HuntingHelperWebService.Eventing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HuntingHelperWebService
{
    public class Startup
    {
        //public static IConnectionManager ConnectionManager;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IApplicationContext, HuntingHelperWebService.ApplicationContext.ApplicationContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            //ConnectionManager = serviceProvider.GetService<IConnectionManager>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection(); // wyłączam to bo mi na https przekierowywało i httpclient mi się nie łączył
            app.UseSignalR((route) => {
                route.MapHub<EventingHub>("/Events");///signalr
            });
            app.UseMvc();
        }
    }
}
