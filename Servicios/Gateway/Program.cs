using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddCors(options =>
                    {
                        options.AddPolicy("CorsPolicy",
                            builder =>
                                builder
                                    //.AllowAnyOrigin() cambiado por signalr
                                    .AllowAnyMethod()
                                    .SetIsOriginAllowed(_ => true)
                                    .AllowAnyHeader()
                                    .AllowCredentials());
                    });
                    s.AddMvc();
                    s.AddOcelot();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIIS()
                .Configure(app =>
                {
                    app.UseCors(options =>
                        options
                            //.AllowAnyOrigin() cambiado por signalr
                            .SetIsOriginAllowed(_ => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
                    app.UseHttpsRedirection();
                    app.UseDefaultFiles();
                    app.UseStaticFiles();
                    app.UseMvc();
                    app.UseOcelot().Wait();
                })
                .Build()
                .Run();
        }
    }
}
