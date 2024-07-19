
using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Implementation;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using Comtrade.FinanceTrack.Kafka.Adapter;

namespace Comtrade.FinanceTrack.Budget.Api.Initialization
{
    public static class InitializationComponent
    {
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {

            // SQL
            var sqlConnectionBudget = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BudgetContext>(opts => opts.UseSqlServer(sqlConnectionBudget), ServiceLifetime.Transient);
            services.AddScoped<DbContext, BudgetContext>();

            var serviceProviderBuilder = services.BuildServiceProvider();
            serviceProviderBuilder.GetService<BudgetContext>().Database.Migrate();


            // Common
            //services.AddHttpContextAccessor();
            services.AddTransient<IBudgetService, BudgetService>();
            services.AddTransient<IBudgetRepository, BudgetRepository>();
            services.AddTransient<IKafkaConsumer, KafkaConsumer>();

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
