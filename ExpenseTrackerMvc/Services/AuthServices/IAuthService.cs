using ExpenseTrackerMvc.ViewModels;

namespace ExpenseTrackerMvc.Services.AuthServices
{
    public interface IAuthService
    {
        Task<string?> Register(RegisterViewModel model);
        Task<string?> Login(LoginViewModel model);
    }
}
