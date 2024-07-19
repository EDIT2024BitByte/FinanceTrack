using Comtrade.FinanceTrack.ViewModel.CashFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.BusinessLogic.Abstraction
{
    public interface ICashFlowService
    {
        Task<List<CategoriesViewModel>> GetAllCategories();
        Task<List<IncomeViewModel>> GetAllIncome(long budgetId);
        Task<List<ExpenseViewModel>> GetAllExpenses(long budgetId);
        Task<IncomeViewModel> AddEditIncome(IncomeViewModel income);
        Task<ExpenseViewModel> AddEditExpense(ExpenseViewModel expense);
        Task<long> DeleteIncome(long incomeId);
        Task<long> DeleteExpense(long expenseId);
        Task<IncomeExpenseDataViewModel> GetIncomeExpenseDataStatistics(long budgetId);
        Task<List<ExpenseViewModel>> GetExpenseByDate(DashboardDateViewModel data);
    }
}
