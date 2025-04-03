using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Repository;

namespace ExpenseTrackerMvc.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int userId)
        {
            return await _repository.FindAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Category>> SearchCategoriesAsync(int userId, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllCategoriesAsync(userId);

            searchTerm = searchTerm.ToLower();
            return await _repository.FindAsync(c =>
                c.UserId == userId &&
                (c.Name.ToLower().Contains(searchTerm) ||
                 c.Description.ToLower().Contains(searchTerm) ||
                 c.Type.ToLower().Contains(searchTerm)));
        }

        public async Task<Category?> GetCategoryByIdAsync(int id, int userId)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null && category.UserId != userId)
                return null;
            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _repository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id, int userId)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null && category.UserId == userId)
            {
                await _repository.DeleteAsync(category);
            }
        }

        public async Task<bool> CategoryExistsAsync(int id, int userId)
        {
            var category = await _repository.GetByIdAsync(id);
            return category != null && category.UserId == userId;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByTypeAsync(int userId, string type)
        {
            return await _repository.FindAsync(c => c.UserId == userId && c.Type == type);
        }
    }
}