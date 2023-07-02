using Api.ApplicationLogic.Interface;
using Api.Core.Entities;
using Api.Infrastructure;
using Api.Presentation.Constants;
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
                string json = File.ReadAllText(LinkConstants.UserData);
                List<User> users = JsonSerializer.Deserialize<List<User>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(async () =>
                {
                    await _unitOfWork.UserRepository.AddRangeAsync(users);
                });
            };
        }
    }
}