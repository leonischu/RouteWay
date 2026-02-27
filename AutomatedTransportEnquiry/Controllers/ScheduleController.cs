using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTransportEnquiry.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>>GetAll()
        {
            var response = await _service.GetAsync();
            return Ok(response);
        }



        [HttpGet("{from}")]
        public async Task<IActionResult> GetAll(string from,string to)
        { 
            var response = await _service.GetAllAsync(from,to);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult>Create(ScheduleCreateDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode((int)response.StatusCode, response);

        }
        [HttpDelete("{scheduleId}")]
        public async Task<ActionResult<APIResponse>>Delete(int scheduleId)
        {
            var response = await _service.DeleteAsync(scheduleId);
            return StatusCode((int)response.StatusCode, response);
        }


    }
}
