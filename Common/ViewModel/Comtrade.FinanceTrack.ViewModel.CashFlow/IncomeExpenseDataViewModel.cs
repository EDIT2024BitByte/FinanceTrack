using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.ViewModel.CashFlow
{
    public class IncomeExpenseDataViewModel
    {
        public decimal IncomeSum {  get; set; }
        public decimal ExpenseSum { get; set; }
        public List<IncomeExpenseTransactionsViewModel> IncomeExpenseTransactions { get; set; }

    }
}
