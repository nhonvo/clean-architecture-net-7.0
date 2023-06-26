using Api.Core.Models;
using Api.Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controller
{
    public class AuthController : BaseController
    {
        private readonly IUserWriteService _userWriteService;

        public AuthController(IUserWriteService userWriteService)
        {
            _userWriteService = userWriteService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginRequest request)
            => Ok(await _userWriteService.Authenticate(request));
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
            => Ok(await _userWriteService.Register(request));
    }
}