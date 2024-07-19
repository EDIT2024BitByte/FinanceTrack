using Microsoft.EntityFrameworkCore;

namespace Comtrade.FinanceTrack.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Commit()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
                this._dbContext = null;
            }
        }
    }
}
