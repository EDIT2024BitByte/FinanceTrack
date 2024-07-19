using AutoMapper;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Implementation;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Repositories;
using Comtrade.FinanceTrack.Budget.UnitOfWork;
using Comtrade.FinanceTrack.Kafka.Adapter;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using Comtrade.FinanceTrack.Mapper.Budget;
using Comtrade.FinanceTrack.Services.Increase.Interfaces;
using Comtrade.FinanceTrack.Services.Increase.Services;
using Comtrade.FinanceTrack.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.SyncService.Inc.Init
{
    public static class InitializationComponent
    {
        public static ServiceProvider Initialize(IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<IBudgetIncreaseConsumeService, BudgetIncreaseConsumeService>();
            serviceProvider.AddTransient<IBudgetSyncService, BudgetSyncService>();
            serviceProvider.AddTransient<IBudgetRepository, BudgetRepository>();
            serviceProvider.AddTransient<IKafkaConsumer, KafkaConsumer>();
            serviceProvider.AddSingleton<IUnitOfWorkProvider<IUnitOfWorkBudget>, UnitOfWorkProvider<IUnitOfWorkBudget, UnitOfWorkBudget>>();

            var configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();

            var sqlConnectionBudget = configuration.GetConnectionString("DefaultConnection");
            serviceProvider.AddDbContext<BudgetContext>(opts => opts.UseSqlServer(sqlConnectionBudget), ServiceLifetime.Transient);
            serviceProvider.AddTransient<DbContext, BudgetContext>();


            serviceProvider.Configure<KafkaAdapterConfig>(opt =>
            {
                var section = configuration.GetSection("Kafka");
                opt.GroupId = section.GetSection("GroupId").Value;
                opt.KafkaServer = section.GetSection("KafkaServer").Value;
                opt.Username = section.GetSection("Username").Value;
                opt.Password = section.GetSection("Password").Value;
            });

            serviceProvider.Configure<TopicsConfig>(opt =>
            {
                var section = configuration.GetSection("Topics");
                opt.IncreaseBudget = section.GetSection("IncreaseBudget").Value;
                opt.DecreaseBudget = section.GetSection("DecreaseBudget").Value;
            });

            serviceProvider.AddLogging(option =>
            {
                option.SetMinimumLevel(LogLevel.Error);
                option.AddNLog("nlog.config");

            });

            // Register AutoMappers here.
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BudgetMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceProvider.AddSingleton(mapper);

            var serviceProviderBuilder = serviceProvider.BuildServiceProvider();
            UnitOfWorkProvider<IUnitOfWorkBudget, UnitOfWorkBudget>.SetContextBuilder(() => { return serviceProviderBuilder.GetService<BudgetContext>(); });
            return serviceProviderBuilder;
        }
    }
}
