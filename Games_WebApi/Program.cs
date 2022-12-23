using Games_WebApi.Rezerv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Games_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() => { RezervDbManager.createIntermediateCopy(); });
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(GameShopContext.urls);
                });
    }
}
