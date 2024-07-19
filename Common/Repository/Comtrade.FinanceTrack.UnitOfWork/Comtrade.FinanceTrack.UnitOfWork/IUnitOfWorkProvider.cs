using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comtrade.FinanceTrack.UnitOfWork
{
    public interface IUnitOfWorkProvider<T_IUnitOfWork>
    {
        T_IUnitOfWork Begin();
    }
}
