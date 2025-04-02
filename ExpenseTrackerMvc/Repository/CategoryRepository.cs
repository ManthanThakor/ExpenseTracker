//using ExpenseTrackerMvc.Data;
//using ExpenseTrackerMvc.Models;
//using Microsoft.EntityFrameworkCore;

//namespace ExpenseTrackerMvc.Repositories
//{
//    public class CategoryRepository : ICategoryRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public CategoryRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Category>> GetAllAsync(string userId)
//        {
//            return await _context.Categories
//                .Where(c => c.UserId == userId)
//                .OrderBy(c => c.Name)
//                .ToListAsync();
//        }

//        public async Task<Category?> GetByIdAsync(int id, string userId)
//        {
//            return await _context.Categories
//                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
//        }

//        public async Task<bool> CreateAsync(Category category)
//        {
//            _context.Categories.Add(category);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> UpdateAsync(Category category)
//        {
//            _context.Categories.Update(category);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> DeleteAsync(int id, string userId)
//        {
//            var category = await GetByIdAsync(id, userId);
//            if (category == null)
//                return false;

//            _context.Categories.Remove(category);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> CategoryExistsAsync(int id, string userId)
//        {
//            return await _context.Categories.AnyAsync(c => c.Id == id && c.UserId == userId);
//        }

//        public async Task<int> CountCategoriesAsync(string userId)
//        {
//            return await _context.Categories.CountAsync(c => c.UserId == userId);
//        }

//        public async Task<IEnumerable<Category>> GetCategoriesByTypeAsync(string userId, string type)
//        {
//            return await _context.Categories
//                .Where(c => c.UserId == userId && c.Type == type)
//                .OrderBy(c => c.Name)
//                .ToListAsync();
//        }
//    }
//}

