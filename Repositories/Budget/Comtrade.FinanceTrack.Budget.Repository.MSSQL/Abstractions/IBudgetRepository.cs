using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions
{
    public interface IBudgetRepository
    {
        Task<List<Models.Budget>> GetAllBudgets();
        Task<Models.Budget> GetBudgetById(long Id);
        Task<bool> CheckBudget(long userId);
        Task<Models.Budget> SaveBudget(Models.Budget budget);
        Task<Models.Budget> GetBudgetByUserId(long userId);
        Task<bool> IncreaseBudget(Models.Budget budget);
        Task<bool> DecreaseBudget(Models.Budget budget);
    }
}
