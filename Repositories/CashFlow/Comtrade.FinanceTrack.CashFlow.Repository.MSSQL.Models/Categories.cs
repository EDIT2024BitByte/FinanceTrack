using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models
{
    public class Categories
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
