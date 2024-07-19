using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

using NLog.Web;

using Ocelot.DependencyInjection;

using Ocelot.Middleware;

using Ocelot.Values;
 
 
var logger = NLogBuilder

                    .ConfigureNLog("nlog.config")

                    .GetCurrentClassLogger();

IConfiguration configuration = new ConfigurationBuilder()

    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)

    .AddJsonFile("ocelot.json", optional: true, reloadOnChange: true).Build();

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddOcelot(configuration);

builder.Services.AddControllers();

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigins", builder =>

{

    builder.AllowAnyOrigin()

     .AllowAnyHeader()

     .AllowAnyMethod();

}));


var app = builder.Build();


app.UseCors("AllowAnyOrigins");

app.UseAuthorization();

app.UseOcelot().Wait();

app.MapControllers();

try

{

    logger.Debug("Comtrade.FinanceTrack.Gateway is about to start...");

    app.Run();

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
