using Api.ApplicationLogic.Interface;
using Api.Infrastructure.Interface;

namespace Api.ApplicationLogic.Services
{
    public class UserReadService : IUserReadService
    {
        private readonly IUserRepository _userRepository;

        public UserReadService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}