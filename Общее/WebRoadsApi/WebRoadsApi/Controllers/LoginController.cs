using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebRoadsApi.ClassHelpers;
using WebRoadsApi.Data;
using WebRoadsApi.Models;

namespace WebRoadsApi.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        PrbContext _db = new PrbContext();

        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Simulate user authentication (replace with actual authentication logic)
            if (request.Username == "admin" && request.Password == "password")
            {
                // Generate JWT token
                var token = _jwtTokenGenerator.GenerateToken("1", "admin@example.com", "Admin");

                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
