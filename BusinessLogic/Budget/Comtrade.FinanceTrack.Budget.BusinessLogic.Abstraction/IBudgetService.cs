using Comtrade.FinanceTrack.ViewModel.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.BusinessLogic.Abstraction
{
    public interface IBudgetService
    {
        Task<List<BudgetViewModel>> GetAllBudgets();
        Task<bool> CheckBudget(long userId);
        Task<BudgetViewModel> SaveBudget(BudgetViewModel budget);
        Task<BudgetViewModel> GetBudgetByUserId(long userId);
        Task<bool> IncreaseBudget(BudgetViewModel budget);
        Task<bool> DecreaseBudget(BudgetViewModel budget);
    }
}
