using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Implementation;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Repositories;
using Comtrade.FinanceTrack.Services.Interfaces;
using Comtrade.FinanceTrack.Services.Services;
using Comtrade.FinanceTrack.SyncService.Dec.Init;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace Comtrade.FinanceTrack.SyncService.Desrease
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    InitializationComponent.Initialize(services);
                    services.AddHostedService<Worker>();

                })
                .ConfigureAppConfiguration((builderContext, config) =>
                {

                    config.AddJsonFile("appsettings.json", true, true).Build();
                });
    }
}
