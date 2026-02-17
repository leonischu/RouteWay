using AutomatedTransportEnquiry.Models;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            var response = await _service.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);

        }

    }
}
