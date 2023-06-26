using Api.ApplicationLogic.Interface;
using Api.Presentation.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controller
{
    public class SeedController : BaseController
    {
        private readonly ISeedService _seedService;
        public SeedController(ISeedService seedService)
        {
            _seedService = seedService;
        }
        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            await _seedService.Seed();
            return Ok(BookConstants.SeedDataSuccessMessage);
        }
    }
}