using Comtrade.FinanceTrack.ViewModel.CashFlow;
using AutoMapper;

namespace Comtrade.FinanceTrack.Mapper.CashFlow
{
    public class CashFlowMappingProfile : Profile
    {
        public CashFlowMappingProfile()
        {
            CreateMap<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Categories, CategoriesViewModel>().ReverseMap();
            CreateMap<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Income, IncomeViewModel>().ReverseMap();
            CreateMap<Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Models.Expense, ExpenseViewModel>().ReverseMap();
        }
    }
}
