using Api.ApplicationLogic.Repositories;
using Api.Infrastructure.IService;

namespace Api.ApplicationLogic.Services
{
    public class UserWriteService : IUserWriteService
    {
        private readonly UserRepository _userRepository;

        public UserWriteService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}