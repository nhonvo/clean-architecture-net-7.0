using Api.Core.Entities;
using Api.Core.Models.AppResult;
using Api.Infrastructure;
using Api.Infrastructure.IService;
using AutoMapper;

namespace Api.ApplicationLogic.Services
{
    public class BookWriteService : IBookWriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookWriteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<int>> Add(BookDTO request)
        {
            var book = _mapper.Map<Book>(request);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.BookRepository.AddAsync(book);
            });
            return new ApiSuccessResult<int>(book.Id);
        }
        public async Task<ApiResult<BookDTO>> Update(Book request)
        {
            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            book = _mapper.Map<Book>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.BookRepository.Update(book);
            });
            var result = _mapper.Map<BookDTO>(book);
            return new ApiSuccessResult<BookDTO>(result);
        }
        public async Task<ApiResult<int>> Delete(int id)
        {
            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.BookRepository.Delete(book);
            });
            return new ApiSuccessResult<int>(book.Id);
        }
    }
}