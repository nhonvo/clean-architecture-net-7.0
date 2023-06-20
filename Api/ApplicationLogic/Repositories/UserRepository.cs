using Api.Core.Entities;
using Api.Infrastructure.Interface;
using Api.Infrastructure.Persistence;

namespace Api.ApplicationLogic.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}