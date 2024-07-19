using Comtrade.FinanceTrack.CashFlow.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.CashFlow.BusinessLogic.Implementation;
using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Repositories;
using Comtrade.FinanceTrack.Kafka.Adapter;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Api.Initialization
{
    public static class InitializationComponent
    {
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {

            // SQL
            var sqlConnectionBudget = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CashFlowContext>(opts => opts.UseSqlServer(sqlConnectionBudget), ServiceLifetime.Transient);
            services.AddScoped<DbContext, CashFlowContext>();

            var serviceProviderBuilder = services.BuildServiceProvider();
            serviceProviderBuilder.GetService<CashFlowContext>().Database.Migrate();

            // Common
            //services.AddHttpContextAccessor();
            services.AddTransient<ICashFlowService, CashFlowService>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IIncomeRepository, IncomeRepository>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IKafkaConsumer, KafkaConsumer>();
            services.AddTransient(typeof(IKafkaProducer<>), typeof(KafkaProducer<>));

            services.Configure<KafkaAdapterConfig>(opt =>
            {
                var section = configuration.GetSection("Kafka");
                opt.GroupId = section.GetSection("GroupId").Value;
                opt.KafkaServer = section.GetSection("KafkaServer").Value;
                opt.Username = section.GetSection("Username").Value;
                opt.Password = section.GetSection("Password").Value;
            });

            services.Configure<TopicsConfig>(opt =>
            {
                var section = configuration.GetSection("Topics");
                opt.IncreaseBudget = section.GetSection("IncreaseBudget").Value;
                opt.DecreaseBudget = section.GetSection("DecreaseBudget").Value;
            });

        }
    }
}
