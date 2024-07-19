using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions
{
    public interface IIncomeRepository
    {
        Task<List<Models.Income>> GetAllIncomes(long budgetId);
        Task<Models.Income> GetIncomeById(long Id);
        Task<decimal> GetIncomeAmountById(long Id);
        Task<Models.Income> AddEditIncome(Models.Income income);
    }
}
