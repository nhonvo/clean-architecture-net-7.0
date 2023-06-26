using Api.ApplicationLogic.Interface;
using Api.Core;
using Api.Core.Entities;
using Api.Core.Utilities;
using Api.Infrastructure.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.User;
using System.Transactions;

namespace Api.ApplicationLogic.Services
{
    public class UserWriteService : IUserWriteService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AppConfiguration _configuration;
        private readonly ICurrentTime _currentTime;
        public UserWriteService(IUserRepository userRepository,
            UserManager<User> userManager,
            IMapper mapper,
            AppConfiguration configuration,
            ICurrentTime currentTime)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _currentTime = currentTime;
        }
        public async Task<UserDTO> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new TransactionException("User does not exist!");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result is false)
                throw new TransactionException("Username or Password Incorrect!");

            var roles = await _userManager.GetRolesAsync(user);
            var token = user.Authenticate(
                roles,
                _configuration.Jwt.Issuer,
                _configuration.Jwt.Audience,
                _configuration.Jwt.Key,
                _currentTime);
            var response = _mapper.Map<UserDTO>(user);
            response.Token = token;

            return response;
        }

        public async Task<UserDTO> Register(RegisterRequest request)
        {
            var findUserName = await _userManager.FindByNameAsync(request.UserName);
            var findEmail = await _userManager.FindByEmailAsync(request.Email);

            var isUserNameExists = findUserName != null;
            if (isUserNameExists)
                throw new TransactionException("This Username Already Used!");

            var isEmailExists = findEmail != null;
            if (isEmailExists)
                throw new TransactionException("This Email Already Used");

            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            var response = _mapper.Map<UserDTO>(user);

            if (result.Succeeded)
                return response;

            throw new TransactionException(string.Join(' ', result.Errors.Select(error => error.Description)));
        }
    }
}