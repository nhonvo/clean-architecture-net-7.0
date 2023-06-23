using Api.Core.Entities;
using Api.Core.Models;
using Api.Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controller
{
    public class BookController : BaseController
    {
        private readonly IBookReadService _bookReadService;
        private readonly IBookWriteService _bookWriteService;
        public BookController(
            IBookReadService bookReadService,
            IBookWriteService bookWriteService)
        {
            _bookReadService = bookReadService;
            _bookWriteService = bookWriteService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _bookReadService.Get(id));

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10)
            => Ok(await _bookReadService.Get(pageIndex, pageSize));

        [HttpPost]
        public async Task<IActionResult> Add(BookDTO request)
            => Ok(await _bookWriteService.Add(request));

        [HttpPut]
        public async Task<IActionResult> Update(Book request)
            => Ok(await _bookWriteService.Update(request));

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _bookWriteService.Delete(id));
    }
}