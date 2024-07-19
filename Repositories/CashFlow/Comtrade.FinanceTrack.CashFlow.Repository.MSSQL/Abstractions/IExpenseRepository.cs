using Comtrade.FinanceTrack.ViewModel.CashFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions
{
    public interface IExpenseRepository
    {
        Task<List<Models.Expense>> GetAllExpenses(long budgetId);
        Task<Models.Expense> GetExpenseById(long Id);
        Task<decimal> GetExpenseAmountById(long Id);
        Task<Models.Expense> AddEditExpense(Models.Expense expense);
        Task<List<Models.Expense>> GetExpenseByDate(DashboardDateViewModel data);
    }
}
