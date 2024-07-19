using Comtrade.FinanceTrack.Helper.Parmeters;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using Comtrade.FinanceTrack.Services.Increase.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Microsoft.Extensions.Logging;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Comtrade.FinanceTrack.Services.Increase.Services
{
    public class BudgetIncreaseConsumeService : IBudgetIncreaseConsumeService
    {
        private readonly ILogger<BudgetIncreaseConsumeService> _logger;
        private readonly IKafkaConsumer _kafkaConsumer;
        private readonly TopicsConfig _serviceTopics;
        private readonly IServiceProvider _serviceProvider;
        public BudgetIncreaseConsumeService(
            IServiceProvider serviceProvider,
            ILogger<BudgetIncreaseConsumeService> logger,
            IKafkaConsumer kafkaConsumer,
            IOptions<TopicsConfig> serviceTopics)
        {
            _logger = logger;
            _kafkaConsumer = kafkaConsumer;
            _serviceTopics = serviceTopics.Value;
            _serviceProvider = serviceProvider;

        }

        public async Task Synchronize()
        {
            try
            {
               await  Task.Run(() => _kafkaConsumer.Consume(_serviceTopics.IncreaseBudget, ConsumeEventIncrease));
            }
            catch (OperationCanceledException ex)
            {
                GlobalParameters.SyncServiceActive = false;
                _logger.LogError(ex, "BudgetIncreaseConsumeService.ProduceException error: ");

            }
            catch (AggregateException aex)
            {
                GlobalParameters.SyncServiceActive = false;
                _logger.LogError(aex, "BudgetIncreaseConsumeService.AggregateException error: ");

            }
            catch (Exception ex)
            {
                GlobalParameters.SyncServiceActive = false;
                _logger.LogError(ex, "BudgetIncreaseConsumeService.Exception error: ");

            }
        }

        public async void ConsumeEventIncrease(ConsumeResult<Ignore, string> message)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var servicePerMessage = scope.ServiceProvider.GetRequiredService<IBudgetSyncService>();
                servicePerMessage.IncreaseBudget(message);
                scope.Dispose();
            }
        }
    }
}
