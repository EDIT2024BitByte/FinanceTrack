using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comtrade.FinanceTrack.Helper.Parmeters;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using Comtrade.FinanceTrack.Services.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.Helper.Parameters;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Comtrade.FinanceTrack.ViewModel.Budget;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Comtrade.FinanceTrack.Services.Services
{
    public class BudgetDecreaseConsumeService : IBudgetDecreaseConsumeService
    {
        public Task Synchronize()
        {
            throw new NotImplementedException();
        }
    }
}
