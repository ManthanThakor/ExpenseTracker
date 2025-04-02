using ExpenseTrackerMvc.Models;

namespace ExpenseTrackerMvc.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(string userId);
        Task<Category?> GetByIdAsync(int id, string userId);
        Task<bool> CreateAsync(Category category);
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id, string userId);
        Task<bool> CategoryExistsAsync(int id, string userId);
        Task<int> CountCategoriesAsync(string userId);
        Task<IEnumerable<Category>> GetCategoriesByTypeAsync(string userId, string type);
    }
}
