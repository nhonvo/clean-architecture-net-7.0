using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Entities;

namespace Api.Infrastructure.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        
    }
}