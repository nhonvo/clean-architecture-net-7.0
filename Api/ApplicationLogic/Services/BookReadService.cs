using Api.ApplicationLogic.Interface;
using Api.Core;
using Api.Core.Commons;
using Api.Core.Entities;
using Api.Infrastructure;
using Api.Infrastructure.Extensions;
using Api.Presentation.Constants;
using AutoMapper;
using Serilog;

namespace Api.ApplicationLogic.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private bool _redisOption;

        public BookReadService(IUnitOfWork unitOfWork,
                               IMapper mapper,
                               ICacheService cacheService,
                               AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            _redisOption = configuration.UseRedisCache;
        }
        public async Task<Pagination<Book>> Get(int pageIndex, int pageSize)
        {
            Log.Information("Getting Pagination for " + pageIndex + " " + pageSize + " pages ");
            if (!_redisOption)
            {
                return await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);
            }

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
            var result = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            NewRelicExtension.ErrorCustomMonitor("Book", BookConstants.NotFoundMessage);
            if (result == null)
            {
                throw new ArgumentNullException(BookConstants.NotFoundMessage);
            }
            return result;
        }
    }
}