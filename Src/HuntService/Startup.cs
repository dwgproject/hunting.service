using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hunt.ServiceContext;
using Hunt.Data;
using Hunt.Eventing;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using Hunt.Tools;

using HuntRepository.Infrastructure;
using HuntRepository.Data;

namespace HuntingHelperWebService
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
            services.AddDbContext<HuntContext>(options => options.UseSqlServer(Configuration.GetValue<string>("ConnectionString:DefaultConnection")));
            services.AddTransient<IServiceContext, Context>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddMvc(options => {
                options.InputFormatters.Insert(0, new RawJsonBodyInputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            
            services.AddSignalR();   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
                route.MapHub<EventingHub>("/Events");
            });

            //app.UseMvc(); // wykomentowałem bo nie chcę mieć w ścieżce api/ i tak dalej
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "Api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

//Configuration.GetValue<string>("ConnectionString:DefaultConnection"))
            // services.AddDbContext<HuntContext>(options =>
            //     options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=hunting;Trusted_Connection=True;MultipleActiveResultSets=true"));




// DbInitializer.Ini
// using (var serviceScope = app.ApplicationServices.CreateScope())
// {
//     HuntContext huntContext = serviceScope.ServiceProvider.GetService<HuntContext>();
//     huntContext.Database.EnsureCreated();       
// }


            // using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            // {
            //     var context = serviceScope.ServiceProvider.GetRequiredService<HuntContext>();
            //     context.Database.EnsureCreated();
            // }