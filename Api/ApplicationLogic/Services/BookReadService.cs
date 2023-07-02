using Api.ApplicationLogic.Interface;
using Api.Core.Commons;
using Api.Core.Entities;
using Api.Infrastructure;
using Api.Presentation.Constants;
using AutoMapper;

namespace Api.ApplicationLogic.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public BookReadService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<Pagination<Book>> Get(int pageIndex, int pageSize)
        {
            var cachedBooks = await _cacheService.Get<Pagination<Book>>("all_books");
            if (cachedBooks != null)
            {
                return cachedBooks;
            }

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            var books = await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);
            await _cacheService.Set("all_books", books, expiryTime);

            return books;
        }
        public async Task<Book> Get(int id)
        {
            return await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ArgumentNullException(BookConstants.NotFoundMessage);
        }
    }
}