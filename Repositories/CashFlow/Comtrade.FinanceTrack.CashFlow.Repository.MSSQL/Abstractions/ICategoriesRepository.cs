using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Abstractions
{
    public interface ICategoriesRepository
    {
        Task<List<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Categories>> GetAllCategories();
        Task<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Categories> GetCategoryById(long Id);
    }
}
