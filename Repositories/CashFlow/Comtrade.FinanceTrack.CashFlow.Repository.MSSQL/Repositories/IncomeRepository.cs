using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Repositories
{
    public class IncomeRepository : BaseRepository<Models.Income, CashFlowContext>, IIncomeRepository
    {
        private readonly ILogger<IncomeRepository> _logger;

        public IncomeRepository(DbContext dbContext, ILogger<IncomeRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<List<Models.Income>> GetAllIncomes(long budgetId)
        {
            return await GetCurrentContext.Income.Where(x => x.BudgetId == budgetId && x.IsDeleted != true).OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<Models.Income> GetIncomeById(long Id)
        {
            return await GetCurrentContext.Income.Where(x=>x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetIncomeAmountById(long Id)
        {
            var income = await GetCurrentContext.Income.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
            GetCurrentContext.Entry(income).State = EntityState.Detached;
            return income.Amount;
        }

        public async Task<Models.Income> AddEditIncome(Models.Income income)
        {
            //Unos/izmena prihoda
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
