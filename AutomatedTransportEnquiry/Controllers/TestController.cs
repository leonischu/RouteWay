using Microsoft.AspNetCore.Mvc;

namespace AutomatedTransportEnquiry.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("bcrypt")]
        public IActionResult TestBcrypt()
        {
            var password = "Admin@123";

            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var verify = BCrypt.Net.BCrypt.Verify(password, hash);

            return Ok(new
            {
                Password = password,
                Hash = hash,
                Verified = verify
            });
        }
    }
}
