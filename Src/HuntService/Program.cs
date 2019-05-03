using GravityZero.HuntingSupport.Service.Main;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace GravityZero.HuntingSupport.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();  
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
