using Api.ApplicationLogic.Repositories;
using Api.Infrastructure.IService;

namespace Api.ApplicationLogic.Services
{
    public class UserReadService : IUserReadService
    {
        private readonly UserRepository _userRepository;

        public UserReadService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}