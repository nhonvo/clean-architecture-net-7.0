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
        private readonly IUnitOfWork _unitOfWork;

        public SeedService(IUnitOfWork unitOfWork)
        {
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
                    UserName = "TruongNhon",
                    Password = "$2a$11$S/EGdnCaX2mhpkkoTSIBLeuPcyCg6rUxBMww3UHdg1yOAK31lG/cO",
                    Email = "vothuongtruongnhon2002@gmail.com",
                    Role = Core.Enum.Role.Admin
                };
                await _unitOfWork.ExecuteTransactionAsync(async () =>
                {
                    await _unitOfWork.UserRepository.AddAsync(user);
                });
            };
        }
    }
}