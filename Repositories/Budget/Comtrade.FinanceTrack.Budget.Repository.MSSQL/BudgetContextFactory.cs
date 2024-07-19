using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.Repository.MSSQL
{
    public class BudgetContextFactory : IDesignTimeDbContextFactory<BudgetContext>
    {
        public BudgetContext CreateDbContext(string[] args) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<BudgetContext>();
            optionsBuilder.UseSqlServer(@"Server=.;Database=FinanceTrackBudget;Trusted_Connection=False;Integrated Security=SSPI;TrustServerCertificate=True;");
            return new BudgetContext(optionsBuilder.Options);

        }

    }
}
