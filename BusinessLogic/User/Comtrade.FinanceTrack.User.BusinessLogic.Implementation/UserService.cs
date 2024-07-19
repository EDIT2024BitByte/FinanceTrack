using AutoMapper;
using Comtrade.FinanceTrack.User.BusinessLogic.Abstraction;
using Comtrade.FinanceTrack.User.Repository.MSSQL.Abstractions;
using Comtrade.FinanceTrack.ViewModel.User;
using Microsoft.Extensions.Logging;

namespace Comtrade.FinanceTrack.User.BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return _mapper.Map<List<UserViewModel>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users.");
                throw;
            }
        }

        public async Task<UserViewModel> Login(UserViewModel user)
        {
            try
            {
                var userEM = _mapper.Map<Comtrade.FinanceTrack.User.Repository.MSSQL.Models.User>(user);
                var userFromDB = await _userRepository.Login(userEM);
                return _mapper.Map<UserViewModel>(userFromDB);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                throw;
            }
        }
    }
}
