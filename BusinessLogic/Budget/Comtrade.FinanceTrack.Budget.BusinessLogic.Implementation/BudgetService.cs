using AutoMapper;
using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.ViewModel.Budget;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.BusinessLogic.Implementation
{
    public class BudgetService: IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BudgetService> _logger;

        public BudgetService(IBudgetRepository budgetRepository, IMapper mapper, ILogger<BudgetService> logger) 
        {
            _budgetRepository = budgetRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BudgetViewModel>> GetAllBudgets()
        {
            try
            {
                var budgets = await _budgetRepository.GetAllBudgets();
                return _mapper.Map<List<BudgetViewModel>>(budgets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all budgets.");
                throw;
            }
        }

        public async Task<bool> CheckBudget(long userId)
        {
            try
            {
                var result = await _budgetRepository.CheckBudget(userId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking budget.");
                throw;
            }
        }

        public async Task<BudgetViewModel> SaveBudget(BudgetViewModel budget)
        {
            try
            {
                var result = await _budgetRepository.SaveBudget(_mapper.Map<Repository.MSSQL.Models.Budget>(budget));
                return _mapper.Map<BudgetViewModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving budget.");
                throw;
            }
        }

        public async Task<BudgetViewModel> GetBudgetByUserId(long userId)
        {
            try
            {
                var result = await _budgetRepository.GetBudgetByUserId(userId);
                return _mapper.Map<BudgetViewModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning budget.");
                throw;
            }
        }

        public async Task<bool> IncreaseBudget(BudgetViewModel budget)
        {
            //Pronalazak konkretnog budzeta u bazi pozivanjem GetBudgetById metode iz Budget Data access layer-a
            //Update glavne sume pozivanjem IncreaseBudget metode iz Budget Data access layer-a
            throw new NotImplementedException("This method is not implemented yet.");
        }

        public async Task<bool> DecreaseBudget(BudgetViewModel budget)
        {
            //Pronalazak konkretnog budzeta u bazi pozivanjem GetBudgetById metode iz Budget Data access layer-a
            //Update glavne sume pozivanjem DecreaseBudget metode iz Budget Data access layer-a
            throw new NotImplementedException("This method is not implemented yet.");
        }


    }
}
