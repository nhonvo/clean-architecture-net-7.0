using Api.ApplicationLogic.Interface;
using Api.Core;
using Api.Core.Entities;
using Api.Core.Utilities;
using Api.Infrastructure;
using Api.Infrastructure.Extensions;
using AutoMapper;
using Models.User;
using Serilog;
using System.Text.Json;
using System.Transactions;

namespace Api.ApplicationLogic.Services
{
    public class UserWriteService : IUserWriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppConfiguration _configuration;
        private readonly ICurrentTime _currentTime;
        public UserWriteService(
            IMapper mapper,
            AppConfiguration configuration,
            ICurrentTime currentTime,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _configuration = configuration;
            _currentTime = currentTime;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDTO> Authenticate(LoginRequest request)
        {
            Log.Information("Request: " + JsonSerializer.Serialize(request));

            var isUserExist = await _unitOfWork.UserRepository.AnyAsync(x => x.UserName == request.UserName);
            if (!isUserExist)
            {
                NewRelicExtension.ErrorCustomMonitor("User", "User does not exist!");
                throw new TransactionException("User does not exist!");
            }

            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (!StringHelper.Verify(request.Password, user.Password))
            {
                NewRelicExtension.ErrorCustomMonitor("User", "User does not exist!");
                throw new TransactionException("Password Incorrect!");
            }

            var token = user.Authenticate(
                _configuration.Jwt.Issuer,
                _configuration.Jwt.Audience,
                _configuration.Jwt.Key,
                _currentTime);
            var response = _mapper.Map<UserDTO>(user);
            response.Token = token;

            Log.Information("Response: " + JsonSerializer.Serialize(response));

            return response;
        }

        public async Task<UserDTO> Register(RegisterRequest request)
        {
            Log.Information("Request: " + JsonSerializer.Serialize(request));

            var isUserExist = await _unitOfWork.UserRepository.AnyAsync(x => x.UserName == request.UserName);
            if (isUserExist)
                throw new TransactionException("This Username Already Used!");

            var isEmailExist = await _unitOfWork.UserRepository.AnyAsync(x => x.UserName == request.Email);
            if (isEmailExist)
                throw new TransactionException("This Email Already Used");


            var user = _mapper.Map<User>(request);
            user.Password = user.Password.Hash();
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.AddAsync(user);
            });

            var response = _mapper.Map<UserDTO>(user);
            Log.Information("Response: " + JsonSerializer.Serialize(response));

            return response;
        }
    }
}