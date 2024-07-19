﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.ViewModel.CashFlow
{
    public class ExpenseViewModel
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public long CategoryId { get; set; }
        public CategoriesViewModel? Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
