using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingCancelController : ControllerBase
    {
        private readonly ICancelBookingService _service;

        public BookingCancelController(ICancelBookingService service)
        {
            _service = service;
        }


        [HttpPut("cancel")]
        public async Task<ActionResult<APIResponse>> Cancel([FromBody] CancelBookingDto dto)
        {
            if (dto == null || dto.BookingId <= 0)
                return BadRequest();

            var result = await _service.CancelBookingAsync(dto);

            return StatusCode((int)result.StatusCode, result);
        }


    }
}
