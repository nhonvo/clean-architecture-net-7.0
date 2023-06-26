using Api.Core.Models;

namespace Api.Infrastructure.IService
{
    public interface IUserWriteService
    {
        Task<UserDTO> Authenticate(LoginRequest request);
        Task<UserDTO> Register(RegisterRequest request);
    }
}