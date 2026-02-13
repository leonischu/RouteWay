using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<ActionResult<IEnumerable<TransportSearchResultDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> Search([FromBody] TransportSearchRequestDto dto )
        {

            if (dto == null || string.IsNullOrWhiteSpace(dto.From) || string.IsNullOrWhiteSpace(dto.To))
                return BadRequest("From and To are required");


            var response = await _service.SearchAsync(dto);
            return Ok(response);
        }
    }
}
