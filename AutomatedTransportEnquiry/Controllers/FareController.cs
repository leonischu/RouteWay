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
    [ProducesResponseType(501)]
    public class FareController : ControllerBase
    {
        private readonly IFareService _service;
        public FareController(IFareService service)
        {
            _service = service; 
        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            return Ok(await _service.GetAllAsync()); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(FareCreateDto dto) 
        {
        
        return Ok(await _service.CreateAsync(dto));

        }
    }
}
