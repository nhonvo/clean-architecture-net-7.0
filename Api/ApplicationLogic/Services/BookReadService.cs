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
    }
}