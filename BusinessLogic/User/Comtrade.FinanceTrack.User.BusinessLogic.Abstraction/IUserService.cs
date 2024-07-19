using Comtrade.FinanceTrack.ViewModel.User;

namespace Comtrade.FinanceTrack.User.BusinessLogic.Abstraction
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUsers();
        Task<UserViewModel> Login(UserViewModel user);
    }
}
