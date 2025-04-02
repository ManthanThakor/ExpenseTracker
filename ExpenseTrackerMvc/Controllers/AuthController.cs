using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerMvc.ViewModels;
using ExpenseTrackerMvc.Services.AuthServices;
using System.Threading.Tasks;

namespace ExpenseTrackerMvc.Controllers
{
    public class AuthController : Controller // Change from ControllerBase to Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Show Login Page
        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        // Show Register Page
        [HttpGet("Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }

        // Handle Register API Call
        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authService.Register(model);
            if (result != null)
            {
                ModelState.AddModelError("", result);
                return View(model); // Show validation error on the page
            }
            return RedirectToAction("Login");
        }

        // Handle Login API Call
        [HttpPost("Auth/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var token = await _authService.Login(model);
            if (token == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetString("JWT", token); // Store JWT in session
            return RedirectToAction("Index", "Home"); // Redirect after login
        }

        // Handle Logout
        [HttpPost("Auth/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            return RedirectToAction("Login");
        }
    }
}
