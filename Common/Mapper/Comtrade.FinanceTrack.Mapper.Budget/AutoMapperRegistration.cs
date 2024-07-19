using AutoMapper;
using Comtrade.FinanceTrack.ViewModel.Budget;


namespace Comtrade.FinanceTrack.Mapper.Budget
{
    public class BudgetMappingProfile:Profile
    {
        public BudgetMappingProfile() 
        {
            CreateMap<Comtrade.FinanceTrack.Budget.Repository.MSSQL.Models.Budget, BudgetViewModel>().ReverseMap();
        }
    }
}
