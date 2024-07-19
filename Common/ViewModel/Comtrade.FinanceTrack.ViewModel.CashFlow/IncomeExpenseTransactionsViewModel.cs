using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.ViewModel.CashFlow
{
    public class IncomeExpenseTransactionsViewModel
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int IncomeExpenseFlag { get; set; }
        public string? Description { get; set; }

    }
}
