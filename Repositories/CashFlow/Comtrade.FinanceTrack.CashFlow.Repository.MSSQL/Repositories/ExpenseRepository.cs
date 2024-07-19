using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Repository;
using Comtrade.FinanceTrack.ViewModel.CashFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Repositories
{
    public class ExpenseRepository : BaseRepository<Models.Expense, CashFlowContext>, IExpenseRepository
    {
        private ILogger<ExpenseRepository> _logger;

        public ExpenseRepository(DbContext dbContext, ILogger<ExpenseRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<List<Models.Expense>> GetAllExpenses(long budgetId)
        {
            return await GetCurrentContext.Expense.Include(x => x.Category).Where(x => x.BudgetId == budgetId && x.IsDeleted != true).OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<Models.Expense> GetExpenseById(long Id)
        {
            return await GetCurrentContext.Expense.Where(x=>x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetExpenseAmountById(long Id)
        {
            var expense = await GetCurrentContext.Expense.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
            GetCurrentContext.Entry(expense).State = EntityState.Detached;
            return expense.Amount;
        }

        public async Task<List<Models.Expense>> GetExpenseByDate(DashboardDateViewModel data)
        {
            try
            {
                return await GetCurrentContext.Expense
                    .Include(x => x.Category)
                    .Where(x => x.BudgetId == data.BudgetId && x.IsDeleted != true &&
                    (data == null ||
                    (data.EndDate.HasValue == false && data.StartDate.HasValue == false) ||
                    (data.EndDate.HasValue == false && data.StartDate.HasValue && x.Date.Date >= data.StartDate.Value.Date) ||
                    (data.EndDate.HasValue && x.Date.Date <= data.EndDate.Value.Date && data.StartDate.HasValue && x.Date.Date >= data.StartDate.Value.Date)))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning expense.");
                throw;
            }
        }
        public async Task<Models.Expense> AddEditExpense(Models.Expense expense)
        {
            //Unos/izmena rashoda
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
