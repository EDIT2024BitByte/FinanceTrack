using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Repositories;
using Comtrade.FinanceTrack.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Comtrade.FinanceTrack.Budget.UnitOfWork
{
    public class UnitOfWorkBudget : Comtrade.FinanceTrack.UnitOfWork.UnitOfWork, IUnitOfWorkBudget
    {
        public UnitOfWorkBudget(DbContext dbContext) : base(dbContext)
        {
        }

        public IBudgetRepository BudgetRepository
        {
            get
            {
                return new BudgetRepository(_dbContext);
            }
        }
    }
}
