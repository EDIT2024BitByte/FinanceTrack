using Comtrade.FinanceTrack.CashFlow.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.ViewModel.CashFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comtrade.FinanceTrack.CashFlow.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashFlowController: ControllerBase
    {
        private readonly ICashFlowService _cashFlowService;
        private readonly ILogger<CashFlowController> _logger;

        public CashFlowController(ICashFlowService cashFlowService, ILogger<CashFlowController> logger)
        {
            _cashFlowService = cashFlowService;
            _logger = logger;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _cashFlowService.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning categories.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllIncome/{budgetId}")]
        public async Task<IActionResult> GetAllIncome([FromRoute] long budgetId)
        {
            try
            {
                var income = await _cashFlowService.GetAllIncome(budgetId);
                return Ok(income);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning the income.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllExpenses/{budgetId}")]
        public async Task<IActionResult> GetAllExpenses([FromRoute] long budgetId)
        {
            try
            {
                var expenses = await _cashFlowService.GetAllExpenses(budgetId);
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning expenses.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteIncome/{incomeId}")]
        public async Task<IActionResult> DeleteIncome([FromRoute] long incomeId)
        {
            try
            {
                var result = await _cashFlowService.DeleteIncome(incomeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting income.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteExpense/{expenseId}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] long expenseId)
        {
            try
            {
                var result = await _cashFlowService.DeleteExpense(expenseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting expense.");
                return StatusCode(500, "An error occurred while processing your expense.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetIncomeExpenseDataStatistics/{budgetId}")]
        /// <summary>
        /// Retrieves income sum, expense sum and last 15 transactions.
        /// </summary>
        public async Task<IActionResult> GetIncomeExpenseDataStatistics([FromRoute] long budgetId)
        {
            try
            {
                var result = await _cashFlowService.GetIncomeExpenseDataStatistics(budgetId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning income and expense data statistic.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetExpenseByDate")]
        public async Task<IActionResult> GetExpenseByDate([FromBody] DashboardDateViewModel data)
        {
            try
            {
                var expense = await _cashFlowService.GetExpenseByDate(data);
                return Ok(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning expense.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddEditIncome")]
        public async Task<IActionResult> AddEditIncome([FromBody] IncomeViewModel income)
        {
            //Poziv AddEditIncome metode u CashFlow biznis logici 
            throw new NotImplementedException("This method is not implemented yet.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddEditExpense")]
        public async Task<IActionResult> AddEditExpense([FromBody] ExpenseViewModel expense)
        {
            //Poziv AddEditExpense metode u CashFlow biznis logici 
            throw new NotImplementedException("This method is not implemented yet.");
        }

    }
}
