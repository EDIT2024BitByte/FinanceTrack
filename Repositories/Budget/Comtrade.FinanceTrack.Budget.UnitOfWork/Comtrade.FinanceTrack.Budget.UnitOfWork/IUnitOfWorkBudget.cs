using Comtrade.FinanceTrack.Budget.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Budget.UnitOfWork
{
    public interface IUnitOfWorkBudget : IUnitOfWork
    {
        IBudgetRepository BudgetRepository { get; }
    }
}
