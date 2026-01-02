using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingController(IBookingService service)
        {
            
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>>Create(BookingCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode((int)result.StatusCode,result);
        }
    }
}
