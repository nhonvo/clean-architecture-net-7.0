using Api.ApplicationLogic.Repositories;
using Api.Infrastructure.Interface;
using Api.Infrastructure.IService;

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