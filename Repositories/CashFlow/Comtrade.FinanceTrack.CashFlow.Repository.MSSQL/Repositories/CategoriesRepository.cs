using Comtrade.FinanceTrack.Repository;
using Microsoft.EntityFrameworkCore;
using Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions;
using Microsoft.Extensions.Logging;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Repositories
{
    public class CategoriesRepository : BaseRepository<Models.Categories, CashFlowContext>, ICategoriesRepository
    {
        private ILogger<CategoriesRepository> _logger;

        public CategoriesRepository(DbContext dbContext, ILogger<CategoriesRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<List<Models.Categories>> GetAllCategories()
        {
            try
            {
                return await GetCurrentContext.Categories.Where(x => x.IsDeleted != true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning categories.");
                throw;
            }
        }

        public async Task<Models.Categories> GetCategoryById(long Id)
        {
            return await GetCurrentContext.Categories.Where(x=>x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
        }
    }
}
