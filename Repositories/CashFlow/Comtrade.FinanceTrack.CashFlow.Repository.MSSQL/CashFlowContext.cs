using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL
{
    public class CashFlowContext : DbContext
    {
        public CashFlowContext(DbContextOptions<CashFlowContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CashFlowContext).Assembly);
        }

        public DbSet<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Categories> Categories { get; set; }
        public DbSet<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Expense> Expense { get; set; }
        public DbSet<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Income> Income { get; set; }
    }
}
