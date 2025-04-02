using ExpenseTrackerMvc.Models;

namespace ExpenseTrackerMvc.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(string userId);
        Task<Category?> GetCategoryByIdAsync(int id, string userId);
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id, string userId);
        Task<bool> CategoryExistsAsync(int id, string userId);
        Task<IEnumerable<Category>> GetCategoriesByTypeAsync(string userId, string type);
        Task<CategoryStatsViewModel> GetCategoryStatsAsync(string userId);
    }
}
