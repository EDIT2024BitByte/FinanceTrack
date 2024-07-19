using AutoMapper;
using Comtrade.FinanceTrack.ViewModel.User;

namespace Comtrade.FinanceTrack.Mapper.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User, UserViewModel>().ReverseMap();
        }
    }
}
