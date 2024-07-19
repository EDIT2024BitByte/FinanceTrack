using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models
{
    public class Income
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
