using Api.ApplicationLogic.Interface;
using Api.Core;
using Api.Core.Entities;
using Api.Infrastructure;
using AutoMapper;
using Models.Book;

namespace Api.ApplicationLogic.Services
{
    public class BookWriteService : IBookWriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private bool _redisOption;

        public BookWriteService(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                ICacheService cacheService,
                                AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            _redisOption = configuration.Options.Redis;
        }
        public async Task<int> Add(BookDTO request)
        {
            var book = _mapper.Map<Book>(request);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.BookRepository.AddAsync(book);
            });
            if (_redisOption)
                await _cacheService.Remove("all_books");
            return book.Id;
        }
        public async Task<BookDTO> Update(Book request)
        {
            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            book = _mapper.Map<Book>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.BookRepository.Update(book);
            });
            var result = _mapper.Map<BookDTO>(book);
            if (_redisOption)
                await _cacheService.Remove("all_books");
            return result;
        }
        public async Task<int> Delete(int id)
        {
            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.BookRepository.Delete(book);
            });
            if (_redisOption)
                await _cacheService.Remove("all_books");
            return book.Id;
        }
    }
}