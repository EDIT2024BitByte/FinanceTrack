using Microsoft.AspNetCore;
using NLog.Web;
using System.Reflection.Metadata;

namespace Comtrade.FinanceTrack.CashFlow.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger =
                NLogBuilder
                    .ConfigureNLog("nlog.config")
                    .GetCurrentClassLogger();

            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables()
           .Build();

            try
            {
                logger.Debug("eSession Core is about to start...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Stopped program because of exception");
                logger.Fatal(ex, $"Error occured: {ex.Message}");
                logger.Fatal(ex.StackTrace);
                while (ex.InnerException != null)
                {
                    logger.Fatal(ex.InnerException.Message);
                    ex = ex.InnerException;
                }
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseNLog();
                });

        }
    }
}
