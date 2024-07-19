using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comtrade.FinanceTrack.UnitOfWork
{
    public class UnitOfWorkProvider<T_IUnitOfWork, T_UnitOfWork> : IUnitOfWorkProvider<T_IUnitOfWork> where T_UnitOfWork : T_IUnitOfWork
    {
        private static Func<DbContext> _contextBuilder;

        public T_IUnitOfWork Begin()
        {
            var _unitOfWork =  (T_UnitOfWork)Activator.CreateInstance(typeof(T_UnitOfWork), new object[] { _contextBuilder() });
            return _unitOfWork;
        }

        public static void SetContextBuilder(Func<DbContext> contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }
    }
}
