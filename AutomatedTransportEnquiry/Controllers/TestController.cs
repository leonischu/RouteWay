using AutomatedTransportEnquiry.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TestController : ControllerBase
    {
        private readonly DapperContext _context;
        public TestController(DapperContext dapperContext)
        {
            
            _context = dapperContext;

        }

        [HttpGet("db")]
        public IActionResult TestDb()
        {
            using var connection = _context.CreateConnection();
           // connection.Open();
            return Ok("Database Connected Sucessfully!");
        }
    }
}
