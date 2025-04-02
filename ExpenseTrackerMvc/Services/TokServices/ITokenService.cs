using ExpenseTrackerMvc.Models;

namespace ExpenseTrackerMvc.Services.TokServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
