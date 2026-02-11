using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTransportEnquiry.Controllers

{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingController(IBookingService service)
        {
            
            _service = service;
        }
        [HttpGet]

        public async Task<ActionResult<APIResponse>>GetAll()
        {
            var response = await _service.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);
        }



        [HttpGet("{id}")]
         
        public async Task<ActionResult<APIResponse>>GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }



        [HttpPost]
        public async Task<ActionResult<APIResponse>>Create([FromBody] BookingCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode((int)result.StatusCode,result);
        }
    }
}
