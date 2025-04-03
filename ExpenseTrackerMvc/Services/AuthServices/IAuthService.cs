using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.ViewModels;
using System.Security.Claims;

namespace ExpenseTrackerMvc.Services.AuthServices
{
    public interface IAuthService
    {

        Task SignInAsync(User user, HttpContext httpContext, bool rememberMe = false);
        Task SignOutAsync(HttpContext httpContext);
        ClaimsPrincipal GetCurrentUser(HttpContext httpContext);
    }
}
