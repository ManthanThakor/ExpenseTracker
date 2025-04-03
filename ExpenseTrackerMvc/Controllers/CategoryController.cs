using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Services;
using ExpenseTrackerMvc.Services.CategoryServices;
using ExpenseTrackerMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerMvc.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("User is not authenticated properly.");
        }

        // GET: Category
        public async Task<IActionResult> Index(string searchTerm, string categoryType)
        {
            int userId = GetCurrentUserId();
            IEnumerable<Category> categories;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = await _categoryService.SearchCategoriesAsync(userId, searchTerm);
            }
            else if (!string.IsNullOrEmpty(categoryType) && categoryType != "All")
            {
                categories = await _categoryService.GetCategoriesByTypeAsync(userId, categoryType);
            }
            else
            {
                categories = await _categoryService.GetAllCategoriesAsync(userId);
            }

            var viewModel = new CategoryListViewModel
            {
                Categories = categories,
                SearchTerm = searchTerm ?? "",
                CategoryType = categoryType ?? "All"
            };

            return View(viewModel);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            var viewModel = new CategoryViewModel();
            return View("CreateEdit", viewModel);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            int userId = GetCurrentUserId();
            var category = await _categoryService.GetCategoryByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Type = category.Type,
                Color = category.Color,
                Icon = category.Icon
            };

            return View("CreateEdit", viewModel);
        }

        // POST: Category/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = GetCurrentUserId();

                if (viewModel.Id == 0)
                {
                    // Create
                    var newCategory = new Category
                    {
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        Type = viewModel.Type,
                        Color = viewModel.Color,
                        Icon = viewModel.Icon,
                        UserId = userId
                    };

                    await _categoryService.CreateCategoryAsync(newCategory);
                    TempData["SuccessMessage"] = "Category created successfully.";
                }
                else
                {
                    // Edit
                    var existingCategory = await _categoryService.GetCategoryByIdAsync(viewModel.Id, userId);
                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    existingCategory.Name = viewModel.Name;
                    existingCategory.Description = viewModel.Description;
                    existingCategory.Type = viewModel.Type;
                    existingCategory.Color = viewModel.Color;
                    existingCategory.Icon = viewModel.Icon;

                    await _categoryService.UpdateCategoryAsync(existingCategory);
                    TempData["SuccessMessage"] = "Category updated successfully.";
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            int userId = GetCurrentUserId();
            var category = await _categoryService.GetCategoryByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryDeleteViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                ExpensesCount = category.Expenses?.Count ?? 0
            };

            return View(viewModel);
        }

        // POST: Category/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetCurrentUserId();
            await _categoryService.DeleteCategoryAsync(id, userId);
            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}