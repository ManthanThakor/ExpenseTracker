using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerMvc.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _repository;
        private readonly IGenericRepository<Category> _categoryRepository;

        public TransactionService(
            IGenericRepository<Transaction> repository,
            IGenericRepository<Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync(int userId)
        {
            return await _repository.Find(t => t.UserId == userId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _repository.Find(t =>
                t.UserId == userId &&
                t.Date >= startDate &&
                t.Date <= endDate);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int userId, int categoryId)
        {
            return await _repository.Find(t =>
                t.UserId == userId &&
                t.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Transaction>> SearchTransactionsAsync(int userId, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllTransactionsAsync(userId);

            searchTerm = searchTerm.ToLower();
            return await _repository.Find(t =>
                t.UserId == userId &&
                (t.Title.ToLower().Contains(searchTerm)));
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id, int userId)
        {
            var transaction = await _repository.GetById(id);
            if (transaction != null && transaction.UserId != userId)
                return null;
            return transaction;
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return await _repository.Add(transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            await _repository.Update(transaction);
        }

        public async Task DeleteTransactionAsync(int id, int userId)
        {
            var transaction = await _repository.GetById(id);
            if (transaction != null && transaction.UserId == userId)
            {
                await _repository.Delete(transaction);
            }
        }

        public async Task<bool> TransactionExistsAsync(int id, int userId)
        {
            var transaction = await _repository.GetById(id);
            return transaction != null && transaction.UserId == userId;
        }

        public async Task<decimal> GetTotalIncomeAsync(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var incomeCategories = await _categoryRepository.Find(c => c.UserId == userId && c.Type == "Income");
            var incomeCategoryIds = incomeCategories.Select(c => c.Id).ToList();

            var query = await _repository.Find(t =>
                t.UserId == userId &&
                incomeCategoryIds.Contains(t.CategoryId));

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            return query.Sum(t => t.Amount);
        }

        public async Task<decimal> GetTotalExpenseAsync(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var expenseCategories = await _categoryRepository.Find(c => c.UserId == userId && c.Type == "Expense");
            var expenseCategoryIds = expenseCategories.Select(c => c.Id).ToList();

            var query = await _repository.Find(t =>
                t.UserId == userId &&
                expenseCategoryIds.Contains(t.CategoryId));

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            return query.Sum(t => t.Amount);
        }
    }
}