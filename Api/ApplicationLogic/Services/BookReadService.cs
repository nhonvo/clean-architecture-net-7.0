using Api.Core.Commons;
using Api.Core.Entities;
using Api.Infrastructure;
using Api.Infrastructure.IService;
using Api.Presentation.Constants;
using AutoMapper;
using System.Text.Json;

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
        public async Task<Pagination<Book>> Get(int pageIndex, int pageSize)
            => await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);
        public async Task<Book> Get(int id)
            => await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ArgumentNullException(BookConstants.NotFoundMessage);

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
        }
    }
}