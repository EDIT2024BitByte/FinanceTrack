namespace Comtrade.FinanceTrack.User.Repository.MSSQL.Abstractions
{
    public interface IUserRepository
    {
        Task<List<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User>> GetAllUsers();
        Task<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User> Login(Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User user);
        Task<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User> GetUserById(long Id);
    }
}
