using Api.Core.Entities;
using Api.Infrastructure;
using Api.Infrastructure.Interface;

namespace Api.ApplicationLogic.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}