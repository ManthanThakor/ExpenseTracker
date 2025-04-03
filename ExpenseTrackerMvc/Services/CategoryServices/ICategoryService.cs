using ExpenseTrackerMvc.Models;

namespace ExpenseTrackerMvc.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int userId);
        Task<IEnumerable<Category>> SearchCategoriesAsync(int userId, string searchTerm);
        Task<Category?> GetCategoryByIdAsync(int id, int userId);
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id, int userId);
        Task<bool> CategoryExistsAsync(int id, int userId);
        Task<IEnumerable<Category>> GetCategoriesByTypeAsync(int userId, string type);
    }
}
