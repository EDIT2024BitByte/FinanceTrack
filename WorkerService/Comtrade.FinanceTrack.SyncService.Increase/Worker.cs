using Comtrade.FinanceTrack.Budget.Repository.MSSQL;
using Comtrade.FinanceTrack.Helper.Parmeters;
using Comtrade.FinanceTrack.Services.Increase.Interfaces;
using Comtrade.FinanceTrack.Services.Increase.Services;

namespace Comtrade.FinanceTrack.SyncService.Increase
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBudgetIncreaseConsumeService _budgetIncreaseConsumeService;

        public Worker(ILogger<Worker> logger,
            IBudgetIncreaseConsumeService budgetIncreaseConsumeService
            )
        {
            _logger = logger;
            _budgetIncreaseConsumeService = budgetIncreaseConsumeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Run(() => StartBudgetIncreaseConsumer());
                await Task.Delay(1000, stoppingToken);
                
            }
 
        }

        public async Task StartBudgetIncreaseConsumer()
        {
            try
            {
                try
                {
                    await _budgetIncreaseConsumeService.Synchronize();
                }
                catch (OperationCanceledException cxe)
                {
                    GlobalParameters.SyncServiceActive = false;
                    _logger.LogError(cxe, "TaskServiceError.StartBudgetIncreaseConsumer.OperationCanceledException");
                    throw;
                }
            }
            catch (Exception ex)
            {
                GlobalParameters.SyncServiceActive = false;
                _logger.LogError(ex, "TaskServiceError.StartBudgetIncreaseConsumer.Exception");
            }
        }
    }
}
