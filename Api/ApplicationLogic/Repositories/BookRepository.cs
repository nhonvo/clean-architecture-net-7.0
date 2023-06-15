using Api.Core.Entities;
using Api.Infrastructure;
using Api.Infrastructure.Interface;

namespace Api.ApplicationLogic.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}