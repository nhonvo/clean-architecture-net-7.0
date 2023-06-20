using Api.Core.Entities;
using Api.Infrastructure.Interface;
using Api.Infrastructure.Persistence;

namespace Api.ApplicationLogic.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}