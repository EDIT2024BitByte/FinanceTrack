using AutoMapper;
using Comtrade.FinanceTrack.CashFlow.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models;
using Comtrade.FinanceTrack.ViewModel.CashFlow;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using Microsoft.Extensions.Options;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;

namespace Comtrade.FinanceTrack.CashFlow.BusinessLogic.Implementation
{
    public class CashFlowService : ICashFlowService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CashFlowService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IKafkaProducer<BudgetChangeViewModel> _kafkaProducer;
        private readonly TopicsConfig _topicsConfig;

        public CashFlowService(ICategoriesRepository categoriesRepository, 
            IIncomeRepository incomeRepository, 
            IExpenseRepository expenseRepository,
            IMapper mapper, 
            ILogger<CashFlowService> logger,
            IConfiguration configuration,
            IKafkaProducer<BudgetChangeViewModel> kafkaProducer,
            IOptions<TopicsConfig> topicConfig) 
        {
            _categoriesRepository = categoriesRepository;
            _incomeRepository = incomeRepository;
            _mapper = mapper;
            _logger = logger;
            _expenseRepository = expenseRepository;
            _configuration = configuration;
            _kafkaProducer = kafkaProducer;
            _topicsConfig = topicConfig.Value;
        }

        public async Task<List<CategoriesViewModel>> GetAllCategories()
        {
            try
            {
                var categories = await _categoriesRepository.GetAllCategories();
                return _mapper.Map<List<CategoriesViewModel>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning categories.");
                throw;
            }
        }

        public async Task<List<IncomeViewModel>> GetAllIncome(long budgetId)
        {
            try
            {
                var income = await _incomeRepository.GetAllIncomes(budgetId);
                return _mapper.Map<List<IncomeViewModel>>(income);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning the income.");
                throw;
            }
        }

        public async Task<List<ExpenseViewModel>> GetAllExpenses(long budgetId)
        {
            try
            {
                var expenses = await _expenseRepository.GetAllExpenses(budgetId);
                return _mapper.Map<List<ExpenseViewModel>>(expenses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning expenses.");
                throw;
            }
        }

        public async Task<long> DeleteIncome(long incomeId)
        {
            try
            {
                var incomeDB = _incomeRepository.GetIncomeById(incomeId).Result;

                if (incomeDB != null)
                {
                    incomeDB.IsDeleted = true;
                    var income = await _incomeRepository.AddEditIncome(incomeDB);
                    return income.Id;
                }
                else
                {
                    throw new Exception("An error occured while deleting income");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting income.");
                throw;
            }
        }

        public async Task<long> DeleteExpense(long expenseId)
        {
            try
            {
                var expenseDB = _expenseRepository.GetExpenseById(expenseId).Result;

                if (expenseDB != null)
                {
                    expenseDB.IsDeleted = true;
                    var expense = await _expenseRepository.AddEditExpense(expenseDB);
                    return expense.Id;
                }
                else
                {
                    throw new Exception("An error occured while deleting expense");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting expense.");
                throw;
            }
        }

        public async Task<IncomeExpenseDataViewModel> GetIncomeExpenseDataStatistics(long budgetId)
        {
            try
            {

                var income = await _incomeRepository.GetAllIncomes(budgetId);
                var expense = await _expenseRepository.GetAllExpenses(budgetId);

                if (income == null)
                {
                    income = new List<Income>(); 
                }

                if (expense == null)
                {
                    expense = new List<Expense>();
                }

                decimal incomeSum = income.Sum(i => i.Amount);
                decimal expenseSum = expense.Sum(e => e.Amount);

                var incomeTransactions = income.Select(i => new IncomeExpenseTransactionsViewModel
                {
                    Date = i.Date,
                    Amount = i.Amount,
                    IncomeExpenseFlag = 1, // 1 for income
                    Description = i.Description
                });

                var expenseTransactions = expense.Select(e => new IncomeExpenseTransactionsViewModel
                {
                    Date = e.Date,
                    Amount = e.Amount,
                    IncomeExpenseFlag = 0, // 0 for expense
                    Description = e.Description
                });

                var allTransactions = incomeTransactions.Concat(expenseTransactions)
                                        .OrderByDescending(t => t.Date)
                                        .Take(15)
                                        .ToList();


                var result = new IncomeExpenseDataViewModel
                {
                    IncomeSum = incomeSum,
                    ExpenseSum = expenseSum,
                    IncomeExpenseTransactions = allTransactions
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning income and expense data statistics.");
                throw;
            }
        }

        public async Task<List<ExpenseViewModel>> GetExpenseByDate(DashboardDateViewModel data)
        {
            try
            {
                var expense = await _expenseRepository.GetExpenseByDate(data);
                return _mapper.Map<List<ExpenseViewModel>>(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning expense.");
                throw;
            }
        }

        public async Task<IncomeViewModel> AddEditIncome(IncomeViewModel income)
        {

            //DAN1 
            /*
                Kreirati novu instancu BudgetChangeViewModel-a kojoj cete dodeliti vrednosti za izmenu budzeta
                Ukoliko se radi izmena, podatke o iznosu vratiti putem metode GetIncomeAmountById iz CashFlow Data access layer-a
                Serijalizovati objekat
                Kreirati url adresu koja sadrzi putanju do BudgetApi-ja (iz konfiguracije) i putanju do metode za povecanje/smanjenje budzeta koristeci klasu UriBuilder
                Koristeci klasu HttpClient pozvati BudgetApi mikroservis i proslediti mu prethodno kreirani url i serijalizovani objekat
                Dodavanje/izmena prihoda pozivanjem AddEditIncome metode iz CashFlow Data access layer-a
             */

            //DAN2
            /*
                Kreirati novu instancu BudgetChangeViewModel-a kojoj cete dodeliti vrednosti za izmenu budzeta
                Ukoliko se radi izmena, podatke o iznosu vratiti putem metode GetIncomeAmountById iz CashFlow Data access layer-a
                Definisati Topic koristeci klasu TopicsConfig 
                    var topic = increaseBudget ? _topicsConfig.IncreaseBudget : _topicsConfig.DecreaseBudget;
                Koristeci Kafku azurirati podatke o budzetu
                    _kafkaProducer.Produce(budgetChange, topic);
                Dodavanje/izmena prihoda pozivanjem AddEditIncome metode iz CashFlow Data access layer-a
             */

            throw new NotImplementedException("This method is not implemented yet.");
        }

        public async Task<ExpenseViewModel> AddEditExpense(ExpenseViewModel expense)
        {
            //DAN1 
            /*
                Kreirati novu instancu BudgetChangeViewModel-a kojoj cete dodeliti vrednosti za izmenu budzeta
                Ukoliko se radi izmena, podatke o iznosu vratiti putem metode GetExpenseAmountById iz CashFlow Data access layer-a
                Serijalizovati objekat
                Kreirati url adresu koja sadrzi putanju do BudgetApi-ja (iz konfiguracije) i putanju do metode za povecanje/smanjenje budzeta koristeci klasu UriBuilder
                Koristeci klasu HttpClient pozvati BudgetApi mikroservis i proslediti mu prethodno kreirani url i serijalizovani objekat
                Dodavanje/izmena rashoda pozivanjem AddEditExpense metode iz CashFlow Data access layer-a
             */

            //DAN2
            /*
                Kreirati novu instancu BudgetChangeViewModel-a kojoj cete dodeliti vrednosti za izmenu budzeta
                Ukoliko se radi izmena, podatke o iznosu vratiti putem metode GetExpenseAmountById iz CashFlow Data access layer-a
                Definisati Topic koristeci klasu TopicsConfig 
                    var topic = increaseBudget ? _topicsConfig.IncreaseBudget : _topicsConfig.DecreaseBudget;
                Koristeci Kafku azurirati podatke o budzetu
                    _kafkaProducer.Produce(budgetChange, topic);
                Dodavanje/izmena rashoda pozivanjem AddEditExpense metode iz CashFlow Data access layer-a
             */

            throw new NotImplementedException("This method is not implemented yet.");
            
        }

    }
}
