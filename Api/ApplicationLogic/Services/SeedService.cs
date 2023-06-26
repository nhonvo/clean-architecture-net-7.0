using Api.ApplicationLogic.Interface;
using Api.Core.Entities;
using Api.Core.Utilities;
using Api.Infrastructure;
using Api.Presentation.Constants;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Api.ApplicationLogic.Services
{
    public class SeedService : ISeedService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public SeedService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task Seed()
        {
            if (!await _unitOfWork.BookRepository.AnyAsync())
            {
                string json = File.ReadAllText(LinkConstants.BookData);
                List<Book> books = JsonSerializer.Deserialize<List<Book>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.BookRepository.AddRangeAsync(books);
                });
            };
            if (!await _unitOfWork.UserRepository.AnyAsync())
            {
                // string json = File.ReadAllText(LinkConstants.UserData);
                // List<User> users = JsonSerializer.Deserialize<List<User>>(json)!;
                User user = new User
                {
                    UserName = "Truong Nhon",
                    PasswordHash = StringHelper.Hash("P@ssw0rd"),
                    PhoneNumber = "0905726748",
                    Email = "vothuongtruongnhon2002@gmail.com"
                };
                await _unitOfWork.ExecuteTransactionAsync(async () =>
                {
                    await _userManager.CreateAsync(user);
                });
                // await _unitOfWork.ExecuteTransactionAsync(async () =>
                // {
                //     await _userManager.AddToRoleAsync(user, "ADMIN");
                // });
            };
        }
    }
}