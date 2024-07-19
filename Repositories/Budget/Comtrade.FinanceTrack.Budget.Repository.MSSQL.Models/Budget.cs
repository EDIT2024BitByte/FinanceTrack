using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.Repository.MSSQL.Models
{
    public class Budget
    {
        public long Id { get; set; }
        public long UserId { get; set;}
        public decimal? TotalAmount { get; set;}
        public bool IsDeleted { get; set; }
    }
}
