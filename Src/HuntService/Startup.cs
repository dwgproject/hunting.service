using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hunt.Eventing;
using GravityZero.HuntingSupport.Repository;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Service.Session;
using GravityZero.HuntingSupport.Service.Context;
using GravityZero.HuntingSupport.Service.Tools;

namespace GravityZero.HuntingSupport.Service.Main
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
            services.AddDbContext<HuntContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetValue<string>("ConnectionString:DefaultConnection")));
            services.AddTransient<IServiceContext, ServiceContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IHuntingRepository, HuntingRepository>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRepository, GravityZero.HuntingSupport.Repository.Repository>();
            services.AddTransient<IAnimalRepository, AnimalRepository>();
            services.AddTransient<IQuarryRepository, QuarryRepository>();
            services.AddTransient<IHuntingService, HuntingService>();
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

            using(var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HuntContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}