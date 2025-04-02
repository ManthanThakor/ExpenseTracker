using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Repositories;

namespace ExpenseTrackerMvc.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(string userId)
        {
            return await _categoryRepository.GetAllAsync(userId);
        }

        public async Task<Category?> GetCategoryByIdAsync(int id, string userId)
        {
            return await _categoryRepository.GetByIdAsync(id, userId);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            return await _categoryRepository.CreateAsync(category);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id, string userId)
        {
            return await _categoryRepository.DeleteAsync(id, userId);
        }

        public async Task<bool> CategoryExistsAsync(int id, string userId)
        {
            return await _categoryRepository.CategoryExistsAsync(id, userId);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByTypeAsync(string userId, string type)
        {
            return await _categoryRepository.GetCategoriesByTypeAsync(userId, type);
        }

        public async Task<CategoryStatsViewModel> GetCategoryStatsAsync(string userId)
        {
            var allCategories = await _categoryRepository.GetAllAsync(userId);
            var incomeCategories = allCategories.Where(c => c.Type == "Income").ToList();
            var expenseCategories = allCategories.Where(c => c.Type == "Expense").ToList();

            return new CategoryStatsViewModel
            {
                TotalCategories = allCategories.Count(),
                IncomeCategories = incomeCategories.Count(),
                ExpenseCategories = expenseCategories.Count()
            };
        }
    }

    public class CategoryStatsViewModel
    {
        public int TotalCategories { get; set; }
        public int IncomeCategories { get; set; }
        public int ExpenseCategories { get; set; }
    }
}
