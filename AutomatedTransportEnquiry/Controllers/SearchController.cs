using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ProducesResponseType(201)]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _service;
        public SearchController(ISearchService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string from, [FromQuery] string to)
        {
            var response = await _service.SearchAsync(from, to);
            return Ok(response);
        }
    }
}
