using Api.ApplicationLogic.Services;
using Api.Core.Entities;

namespace Api.Infrastructure.IService
{
    public interface IBookWriteService
    {
        Task<int> Add(BookDTO request);
        Task<BookDTO> Update(Book request);
        Task<int> Delete(int id);
    }
}