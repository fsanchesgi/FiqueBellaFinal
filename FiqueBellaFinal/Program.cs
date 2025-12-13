using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace FiqueBellaFinal
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
                    var port = Environment.GetEnvironmentVariable("PORT") ?? "80";

                    webBuilder
                        .UseStartup<Startup>()
                        .UseUrls($"http://*:{port}");
                });
    }
}
