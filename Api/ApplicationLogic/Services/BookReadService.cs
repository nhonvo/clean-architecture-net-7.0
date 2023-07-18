using System.Text.Json;
using Api.ApplicationLogic.Interface;
using Api.Core;
using Api.Core.Commons;
using Api.Core.Entities;
using Api.Infrastructure;
using Api.Infrastructure.Extensions;
using Api.Presentation.Constants;
using MessagePack;
using Serilog;


namespace Api.ApplicationLogic.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private bool _redisOption;

        public BookReadService(IUnitOfWork unitOfWork,
                               ICacheService cacheService,
                               AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _redisOption = configuration.UseRedisCache;
        }


        public async Task<Pagination<Book>> Get(int pageIndex, int pageSize)
        {
            Log.Information("Request: " + JsonSerializer.Serialize(new { PageIndex = pageIndex, PageSize = pageSize }));
            var books = await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);

            // Log the books using System.Text.Json serialization
            Log.Information("Response: " + JsonSerializer.Serialize(books));

            return books;
        }

        public async Task<Book> Get(int id)
        {
            Log.Information("Request: " + JsonSerializer.Serialize(id));
            NewRelicExtension.BookErrorMonitor("Book", BookConstants.NotFoundMessage);

            if (!_redisOption)
            {
                var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
                Log.Information("Response: " + JsonSerializer.Serialize(book));
                return book;

            }
            // Attempt to fetch the book from the cache
            var cachedBook = await _cacheService.Get<Book>("book_" + id);
            if (cachedBook != null)
            {
                Log.Information("Response: " + JsonSerializer.Serialize(cachedBook));
                return cachedBook;
            }
            // Book not found in cache, fetch from the database
            var result = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                NewRelicExtension.BookErrorMonitor("Book", BookConstants.NotFoundMessage);
                Log.Error(BookConstants.NotFoundMessage);
                throw new ArgumentNullException(BookConstants.NotFoundMessage);
            }
            // Add the book to the cache with a unique key
            var expiryTime = DateTimeOffset.Now.AddMinutes(3);
            await _cacheService.Set("book_" + id, result, expiryTime);
            Log.Information("Response: " + JsonSerializer.Serialize(result));
            return result;
        }
    }
}