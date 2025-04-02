using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;

namespace ExpenseTrackerMvc.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoryController(ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        private async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id ?? string.Empty;
        }

        // GET: Category
        public async Task<IActionResult> Index(string type = "")
        {
            var userId = await GetCurrentUserId();
            IEnumerable<Category> categories;

            ViewBag.CurrentFilter = type;
            ViewBag.Stats = await _categoryService.GetCategoryStatsAsync(userId);

            if (string.IsNullOrEmpty(type))
            {
                categories = await _categoryService.GetAllCategoriesAsync(userId);
            }
            else
            {
                categories = await _categoryService.GetCategoriesByTypeAsync(userId, type);
            }

            return View(categories);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.UserId = await GetCurrentUserId();
                await _categoryService.CreateCategoryAsync(category);
                TempData["Success"] = "Category created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await GetCurrentUserId();
            var category = await _categoryService.GetCategoryByIdAsync(id, userId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = await GetCurrentUserId();
                category.UserId = userId;

                if (!await _categoryService.CategoryExistsAsync(id, userId))
                {
                    return NotFound();
                }

                await _categoryService.UpdateCategoryAsync(category);
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserId();
            var category = await _categoryService.GetCategoryByIdAsync(id, userId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = await GetCurrentUserId();
            await _categoryService.DeleteCategoryAsync(id, userId);
            TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}