using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {

            await _service.Register(dto);
            return Ok(new { message = "User registered" });


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _service.Login(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[Authorize]

        [HttpGet("detail")]
        public async Task<IActionResult> Detail()
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idClaim))
                return Unauthorized("Invalid token");

            if (!int.TryParse(idClaim, out int userId))
                return Unauthorized("Invalid token");

            var user = await _service.GetUserDetail(userId);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);

        }
    }
}
