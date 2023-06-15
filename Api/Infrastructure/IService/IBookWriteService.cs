using Api.ApplicationLogic.Services;
using Api.Core.Entities;
using Api.Core.Models.AppResult;

namespace Api.Infrastructure.IService
{
    public interface IBookWriteService
    {
        Task<ApiResult<int>> Add(BookDTO request);
        Task<ApiResult<BookDTO>> Update(Book request);
        Task<ApiResult<int>> Delete(int id);
    }
}