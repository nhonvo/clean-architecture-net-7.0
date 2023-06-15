using System.Text.Json;
using Api.Core;
using Api.Core.Entities;
using Api.Core.Models.AppResult;
using Api.Infrastructure;
using Api.Infrastructure.Interface;
using Api.Infrastructure.IService;
using AutoMapper;

namespace Api.ApplicationLogic.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookReadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Pagination<Book>>> Get(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);
            return new ApiSuccessResult<Pagination<Book>>(result);
        }
        public async Task<ApiResult<Book>> Get(int id)
        {
            var result = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return new ApiErrorResult<Book>("Book not found");
            return new ApiSuccessResult<Book>(result);
        }

        public async Task Seed()
        {
            if (!await _unitOfWork.BookRepository.AnyAsync())
            {
                string json = File.ReadAllText(@".\Json\book.json");
                List<Book> books = JsonSerializer.Deserialize<List<Book>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.BookRepository.AddRangeAsync(books);
                });
            };
        }
    }
    public class BookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? Price { get; set; }
    }
}