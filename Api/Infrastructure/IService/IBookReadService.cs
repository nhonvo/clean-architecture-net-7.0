
using Api.Core;
using Api.Core.Entities;
using Api.Core.Models.AppResult;

namespace Api.Infrastructure.IService
{
    public interface IBookReadService
    {
        Task<ApiResult<Pagination<Book>>> Get(int pageIndex, int pageSize);
        Task<ApiResult<Book>> Get(int id);
        Task Seed();
    }
}