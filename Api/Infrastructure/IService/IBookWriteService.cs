using Api.Core.Entities;
using Api.Core.Models;

namespace Api.Infrastructure.IService
{
    public interface IBookWriteService
    {
        Task<int> Add(BookDTO request);
        Task<BookDTO> Update(Book request);
        Task<int> Delete(int id);
    }
}