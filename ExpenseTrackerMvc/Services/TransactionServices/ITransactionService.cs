using ExpenseTrackerMvc.Models;

namespace ExpenseTrackerMvc.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync(int userId);
        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int userId, int categoryId);
        Task<IEnumerable<Transaction>> SearchTransactionsAsync(int userId, string searchTerm);
        Task<Transaction?> GetTransactionByIdAsync(int id, int userId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id, int userId);
        Task<bool> TransactionExistsAsync(int id, int userId);
        Task<decimal> GetTotalIncomeAsync(int userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<decimal> GetTotalExpenseAsync(int userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}