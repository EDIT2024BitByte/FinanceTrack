using System;
using System.Collections.Generic;
using System.Text;

namespace Comtrade.FinanceTrack.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Dispose();
    }
}
