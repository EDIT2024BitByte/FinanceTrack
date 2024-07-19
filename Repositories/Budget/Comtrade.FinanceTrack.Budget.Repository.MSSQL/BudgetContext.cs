using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Comtrade.FinanceTrack.Budget.Repository.MSSQL
{
    public class BudgetContext:DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetContext).Assembly);
        }

        public DbSet<Comtrade.FinanceTrack.Budget.Repository.MSSQL.Models.Budget> Budget { get; set; }
    }
}
