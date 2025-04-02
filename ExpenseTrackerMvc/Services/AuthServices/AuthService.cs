using ExpenseTrackerMvc.Data;
using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Services.PassService;
using ExpenseTrackerMvc.Services.TokServices;
using ExpenseTrackerMvc.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerMvc.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, IPasswordService passwordService, ITokenService tokenService)
        {
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<string?> Register(RegisterViewModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return "Email is already in use.";
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = _passwordService.HashPassword(model.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string?> Login(LoginViewModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !_passwordService.VerifyPassword(model.Password, user.PasswordHash))
            {
                return "Invalid email or password.";
            }

            return _tokenService.GenerateToken(user);
        }
    }
}
