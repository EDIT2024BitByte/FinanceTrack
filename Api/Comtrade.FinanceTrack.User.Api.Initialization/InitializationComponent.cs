using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Comtrade.FinanceTrack.User.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.User.BusinessLogic.Implementation;
using Comtrade.FinanceTrack.User.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.User.Repository.MSSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.Api.Initialization
{
    public static class InitializationComponent
    {
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {

            // SQL
            var sqlConnectionBudget = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(opts => opts.UseSqlServer(sqlConnectionBudget), ServiceLifetime.Transient);
            services.AddScoped<DbContext, UserContext>();

            var serviceProviderBuilder = services.BuildServiceProvider();
            serviceProviderBuilder.GetService<UserContext>().Database.Migrate();

            // Common
            //services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

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
