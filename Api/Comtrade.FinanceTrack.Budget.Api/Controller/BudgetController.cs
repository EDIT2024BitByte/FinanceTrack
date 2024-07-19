using Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.ViewModel.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Comtrade.FinanceTrack.Budget.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly ILogger<BudgetController> _logger;
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService, ILogger<BudgetController> logger)
        {
            _budgetService = budgetService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Gateway running!");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() 
        { 
            try
            {
                var budgets = await _budgetService.GetAllBudgets();
                return Ok(budgets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all budgets.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CheckBudget/{userId}")]
        public async Task<IActionResult> CheckBudget([FromRoute] long userId)
        {
            try
            {
                var result = await _budgetService.CheckBudget(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking budget.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("SaveBudget")]
        public async Task<IActionResult> SaveBudget([FromBody] BudgetViewModel budget)
        {
            try
            {
                var result = await _budgetService.SaveBudget(budget);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving budget.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetBudgetByUserId/{userId}")]
        public async Task<IActionResult> GetBudgetByUserId([FromRoute] long userId)
        {
            try
            {
                var result = await _budgetService.GetBudgetByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning budget.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("IncreaseBudget")]
        public async Task<IActionResult> IncreaseBudget([FromBody] BudgetViewModel budget)
        {
            //Poziv IncreaseBudget metode u Budget biznis logici 
            throw new NotImplementedException("This method is not implemented yet.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DecreaseBudget")]
        public async Task<IActionResult> DecreaseBudget([FromBody] BudgetViewModel budget)
        {
            //Poziv DecreaseBudget metode u Budget biznis logici 
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
