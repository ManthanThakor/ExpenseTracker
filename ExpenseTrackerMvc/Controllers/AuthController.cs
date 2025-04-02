using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerMvc.ViewModels;
using ExpenseTrackerMvc.Services.AuthServices;

namespace ExpenseTrackerMvc.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _authService.Register(model);
            if (result != null) return BadRequest(result);
            return Ok("User registered successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var token = await _authService.Login(model);
            if (token == null) return Unauthorized("Invalid email or password.");
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedRoute()
        {
            return Ok("You have accessed a protected route!");
        }
    }
}
