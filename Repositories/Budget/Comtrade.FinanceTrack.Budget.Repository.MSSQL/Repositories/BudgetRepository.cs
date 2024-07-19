using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Repository;
using Microsoft.EntityFrameworkCore;

namespace Comtrade.FinanceTrack.Budget.Repository.MSSQL.Repositories
{
    public class BudgetRepository : BaseRepository<Models.Budget, BudgetContext>, IBudgetRepository
    {
        public BudgetRepository(DbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Models.Budget>> GetAllBudgets()
        {
            try
            {
                return await GetCurrentContext.Budget.Where(x => x.IsDeleted != true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Models.Budget> GetBudgetById(long Id)
        {
            try
            {
                return await GetCurrentContext.Budget.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CheckBudget(long userId)
        {
            try
            {
                return await GetCurrentContext.Budget.AnyAsync(x => x.UserId == userId && x.IsDeleted != true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Models.Budget> SaveBudget(Models.Budget budget)
        {
            try
            {
                GetCurrentContext.Budget.Add(budget);
                GetCurrentContext.SaveChanges();

                return budget;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Models.Budget> GetBudgetByUserId(long userId)
        {
            try
            {
                return await GetCurrentContext.Budget.FirstOrDefaultAsync(x => x.UserId == userId && x.IsDeleted != true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> IncreaseBudget(Models.Budget budget)
        {
            //Azurirati budzet u bazi
            throw new NotImplementedException("This method is not implemented yet.");
        }

        public async Task<bool> DecreaseBudget(Models.Budget budget)
        {
            //Azurirati budzet u bazi
            throw new NotImplementedException("This method is not implemented yet.");
        }

    }
}
