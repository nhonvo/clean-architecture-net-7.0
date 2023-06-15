using Api.ApplicationLogic.Repositories;
using Api.Infrastructure.Interface;
using Api.Infrastructure.IService;

namespace Api.ApplicationLogic.Services
{
    public class UserWriteService : IUserWriteService
    {
        private readonly IUserRepository _userRepository;

        public UserWriteService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}