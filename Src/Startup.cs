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
            services.AddSingleton<IServiceContext, ServiceContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Configuration.GetValue<string>("ConnectionString:DefaultConnection"))
            services.AddDbContext<HuntContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=hunting;Trusted_Connection=True;MultipleActiveResultSets=true"));
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

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HuntContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}






// DbInitializer.Ini
// using (var serviceScope = app.ApplicationServices.CreateScope())
// {
//     HuntContext huntContext = serviceScope.ServiceProvider.GetService<HuntContext>();
//     huntContext.Database.EnsureCreated();       
// }


// options=>
//                 {
//                     options
//                     .InputFormatters
//                     .Where(item=>item.GetType() == typeof(JsonInputFormatter))
//                     .Cast<JsonInputFormatter>()
//                     .Single()
//                     .SupportedMediaTypes
//                     .Add("application/x-www-form-urlencoded");
//                 }