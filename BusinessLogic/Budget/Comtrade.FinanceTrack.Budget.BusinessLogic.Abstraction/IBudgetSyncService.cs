using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction
{
    public interface IBudgetSyncService
    {
        public Task<bool> IncreaseBudget(ConsumeResult<Ignore, string> message);
        public Task<bool> DecreaseBudget(ConsumeResult<Ignore, string> message);
    }
}
