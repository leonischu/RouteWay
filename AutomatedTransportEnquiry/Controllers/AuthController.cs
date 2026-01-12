using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.Login(dto);
            return Ok(new { token });
        }



        [HttpGet("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Verification token is required" });

            var success = await _service.VerifyEmail(token);

            if (!success)
                return BadRequest(new
                {
                    message = "Invalid or expired verification token"
                });

            return Ok(new
            {
                message = "Email verified successfully! You can now login to your account."
            });









        }
    }
}
