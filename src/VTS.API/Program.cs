using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTS.Data;

namespace VTS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: setup the logger 
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Init-Start and Seeding Data");

                var host = CreateHostBuilder(args).Build();
                SeedDatabase(host);
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something went wrong !");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads
                NLog.LogManager.Shutdown();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
               .UseNLog();

        private static void SeedDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDBContext>();

                    DBSeeder.Initialize(context, services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError("An error occurred while seeding the database");
                }
            }
        }

    }
}
