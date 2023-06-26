using Api.Core.Models;

namespace Api.ApplicationLogic.Interface
{
    public interface IUserWriteService
    {
        Task<UserDTO> Authenticate(LoginRequest request);
        Task<UserDTO> Register(RegisterRequest request);
    }
}