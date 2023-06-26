using Api.Core.Commons;
using Api.Core.Entities;

namespace Api.ApplicationLogic.Interface
{
    public interface IBookReadService
    {
        Task<Pagination<Book>> Get(int pageIndex, int pageSize);
        Task<Book> Get(int id);
    }
}