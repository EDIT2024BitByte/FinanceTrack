using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Budget.UnitOfWork;
using Comtrade.FinanceTrack.UnitOfWork;
using Comtrade.FinanceTrack.ViewModel.Budget;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Comtrade.FinanceTrack.Budget.BusinessLogic.Implementation
{
    public class BudgetSyncService : IBudgetSyncService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkProvider<IUnitOfWorkBudget> _unitOfWorkProvider;
        private readonly ILogger<BudgetSyncService> _logger;

        private readonly IBudgetRepository _budgetRepository;
        private static readonly Object obj = new Object();

        public BudgetSyncService(IServiceProvider serviceProvider, 
            IMapper mapper,
            IUnitOfWorkProvider<IUnitOfWorkBudget> unitOfWorkProvider,
            IBudgetRepository budgetRepository,
            ILogger<BudgetSyncService> logger)
        {
            _serviceProvider = serviceProvider;
            _unitOfWorkProvider = unitOfWorkProvider;
            _mapper = mapper;
            _budgetRepository = budgetRepository;
            _logger = logger;
        }


        public async Task<bool> IncreaseBudget(ConsumeResult<Ignore, string> message)
        {
            bool result = false;
            try
            {
                var budgetChange = message != null ? JsonSerializer.Deserialize<BudgetViewModel>(message.Value) : null;
                if (budgetChange != null)
                {
                    lock (obj)
                    {
                        using (var unitOfWork = _unitOfWorkProvider.Begin())
                        {
                            Comtrade.FinanceTrack.Budget.Repository.MSSQL.Models.Budget budgetDB =
                                unitOfWork.BudgetRepository.GetBudgetById(budgetChange.Id).Result;
                            if (budgetDB != null)
                            {
                                budgetDB.TotalAmount += budgetChange.TotalAmount;
                                result = unitOfWork.BudgetRepository.IncreaseBudget(budgetDB).Result;
                            }

                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while increase budget.");
            }
            return result;

        }

        public async Task<bool> DecreaseBudget(ConsumeResult<Ignore, string> message)
        {
            //Pronalazak konkretnog budzeta u bazi pozivanjem GetBudgetById metode iz Budget Data access layer-a
            //Update glavne sume pozivanjem DecreaseBudget metode iz Budget Data access layer-a
            //Po ugledu na prethodnu metodu (IncreaseBudget)
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
