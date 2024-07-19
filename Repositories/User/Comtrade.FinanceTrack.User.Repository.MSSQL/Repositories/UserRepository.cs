using Comtrade.FinanceTrack.Repository;
using Comtrade.FinanceTrack.User.Repository.MSSQL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Comtrade.FinanceTrack.User.Repository.MSSQL.Repositories
{
    public class UserRepository : BaseRepository<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User, UserContext>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DbContext dbContext, ILogger<UserRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<List<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User>> GetAllUsers()
        {
            try
            {
                return await GetCurrentContext.User.Where(x => x.IsDeleted != true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users.");
                throw;
            }
        }

        public async Task<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User> GetUserById(long Id)
        {
            try
            {
                return await GetCurrentContext.User.Where(x => x.Id == Id && x.IsDeleted != true).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving user with ID {Id}.");
                throw;
            }
        }

        public async Task<Models.User> Login(Models.User user)
        {
            try
            {
                var userFromDB = await GetCurrentContext.User
                    .Where(x => x.Username == user.Username && x.Password == user.Password)
                    .FirstOrDefaultAsync();
                return userFromDB;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                throw;
            }
        }
    }
}
