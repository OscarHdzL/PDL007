using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Authentication;

namespace Api_STEAR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseUrls("https://localhost:5005");
                   webBuilder.ConfigureKestrel(option =>
                   {
                       option.ConfigureHttpsDefaults(defaultSetting =>
                       {
                           defaultSetting.SslProtocols = SslProtocols.Tls12;
                       });
                   }).UseStartup<Startup>();
               });
    }
}
