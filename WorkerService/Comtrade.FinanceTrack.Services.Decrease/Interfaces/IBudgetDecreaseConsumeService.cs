using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Services.Interfaces
{
    public interface IBudgetDecreaseConsumeService
    {
        Task Synchronize();
    }
}
