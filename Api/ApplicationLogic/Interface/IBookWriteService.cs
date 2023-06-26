using Api.Core.Entities;
using Models.Book;

namespace Api.ApplicationLogic.Interface
{
    public interface IBookWriteService
    {
        Task<int> Add(BookDTO request);
        Task<BookDTO> Update(Book request);
        Task<int> Delete(int id);
    }
}