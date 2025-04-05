using ExpenseTrackerMvc.Models;
using ExpenseTrackerMvc.Services.CategoryServices;
using ExpenseTrackerMvc.Services.TransactionServices;
using ExpenseTrackerMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ExpenseTrackerMvc.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;

        public TransactionController(
            ITransactionService transactionService,
            ICategoryService categoryService)
        {
            _transactionService = transactionService;
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

        public async Task<IActionResult> Index(TransactionListViewModel? model)
        {
            int userId = GetCurrentUserId();
            IEnumerable<Transaction> transactions;

            // Initialize if model is null
            if (model == null)
            {
                model = new TransactionListViewModel();
            }

            // Set default dates if not provided
            if (!model.StartDate.HasValue)
            {
                model.StartDate = DateTime.Today.AddDays(-30);
            }
            if (!model.EndDate.HasValue)
            {
                model.EndDate = DateTime.Today;
            }

            // Get transactions based on filters
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                transactions = await _transactionService.SearchTransactionsAsync(userId, model.SearchTerm);
            }
            else if (model.CategoryId.HasValue && model.CategoryId.Value > 0)
            {
                transactions = await _transactionService.GetTransactionsByCategoryAsync(userId, model.CategoryId.Value);
            }
            else if (model.StartDate.HasValue && model.EndDate.HasValue)
            {
                transactions = await _transactionService.GetTransactionsByDateRangeAsync(userId, model.StartDate.Value, model.EndDate.Value);
            }
            else
            {
                transactions = await _transactionService.GetAllTransactionsAsync(userId);
            }

            // Apply date filter
            if (model.StartDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date >= model.StartDate.Value);
            }
            if (model.EndDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date <= model.EndDate.Value);
            }

            // Get categories for dropdown
            var categories = await _categoryService.GetAllCategoriesAsync(userId);
            model.Categories = new SelectList(categories, "Id", "Name");

            // Get financial summaries
            model.TotalIncome = await _transactionService.GetTotalIncomeAsync(userId, model.StartDate, model.EndDate);
            model.TotalExpense = await _transactionService.GetTotalExpenseAsync(userId, model.StartDate, model.EndDate);
            model.Balance = model.TotalIncome - model.TotalExpense;

            // Order transactions by date (newest first)
            model.Transactions = transactions.OrderByDescending(t => t.Date).ThenByDescending(t => t.Id);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            int userId = GetCurrentUserId();
            var categories = await _categoryService.GetAllCategoriesAsync(userId);

            TransactionViewModel viewModel = new TransactionViewModel
            {
                Date = DateTime.Today,
                Categories = new SelectList(categories, "Id", "Name")
            };

            return View("CreateEdit", viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            int userId = GetCurrentUserId();
            var transaction = await _transactionService.GetTransactionByIdAsync(id, userId);
            if (transaction == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync(userId);

            TransactionViewModel viewModel = new TransactionViewModel
            {
                Id = transaction.Id,
                Title = transaction.Title,
                Amount = transaction.Amount,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                Categories = new SelectList(categories, "Id", "Name", transaction.CategoryId)
            };

            return View("CreateEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(TransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = GetCurrentUserId();

                if (viewModel.Id == 0)
                {
                    // Create new transaction
                    Transaction newTransaction = new Transaction
                    {
                        Title = viewModel.Title,
                        Amount = viewModel.Amount,
                        Date = viewModel.Date,
                        CategoryId = viewModel.CategoryId,
                        UserId = userId,
                        CreatedAt = DateTime.Now
                    };

                    await _transactionService.CreateTransactionAsync(newTransaction);
                    TempData["SuccessMessage"] = "Transaction created successfully.";
                }
                else
                {
                    // Update existing transaction
                    Transaction? existingTransaction = await _transactionService.GetTransactionByIdAsync(viewModel.Id, userId);
                    if (existingTransaction == null)
                    {
                        return NotFound();
                    }

                    existingTransaction.Title = viewModel.Title;
                    existingTransaction.Amount = viewModel.Amount;
                    existingTransaction.Date = viewModel.Date;
                    existingTransaction.CategoryId = viewModel.CategoryId;

                    await _transactionService.UpdateTransactionAsync(existingTransaction);
                    TempData["SuccessMessage"] = "Transaction updated successfully.";
                }

                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed; reload categories and return view
            int currentUserId = GetCurrentUserId();
            var categories = await _categoryService.GetAllCategoriesAsync(currentUserId);
            viewModel.Categories = new SelectList(categories, "Id", "Name", viewModel.CategoryId);

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            int userId = GetCurrentUserId();
            var transaction = await _transactionService.GetTransactionByIdAsync(id, userId);
            if (transaction == null)
            {
                return NotFound();
            }

            string categoryName = "Unknown";
            if (transaction.Category != null)
            {
                categoryName = transaction.Category.Name;
            }
            else if (transaction.CategoryId > 0)
            {
                var category = await _categoryService.GetCategoryByIdAsync(transaction.CategoryId, userId);
                if (category != null)
                {
                    categoryName = category.Name;
                }
            }

            TransactionDeleteViewModel viewModel = new TransactionDeleteViewModel
            {
                Id = transaction.Id,
                Title = transaction.Title,
                Amount = transaction.Amount,
                Date = transaction.Date,
                CategoryName = categoryName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetCurrentUserId();
            await _transactionService.DeleteTransactionAsync(id, userId);
            TempData["SuccessMessage"] = "Transaction deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Dashboard()
        {
            int userId = GetCurrentUserId();

            // Default to last 30 days
            DateTime startDate = DateTime.Today.AddDays(-30);
            DateTime endDate = DateTime.Today;

            // Calculate totals
            decimal totalIncome = await _transactionService.GetTotalIncomeAsync(userId, startDate, endDate);
            decimal totalExpense = await _transactionService.GetTotalExpenseAsync(userId, startDate, endDate);

            // Get all transactions in date range
            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(userId, startDate, endDate);

            // Get all categories
            var categories = await _categoryService.GetAllCategoriesAsync(userId);

            // Prepare category summaries
            List<CategorySummary> categorySummaries = new List<CategorySummary>();

            // Group transactions by category
            var transactionsByCategory = transactions.GroupBy(t => t.CategoryId);

            foreach (var group in transactionsByCategory)
            {
                var category = categories.FirstOrDefault(c => c.Id == group.Key);
                if (category != null)
                {
                    decimal amount = group.Sum(t => t.Amount);
                    decimal percentage = 0;

                    // Calculate percentage based on type
                    if (category.Type == "Income" && totalIncome > 0)
                    {
                        percentage = (amount / totalIncome) * 100;
                    }
                    else if (category.Type == "Expense" && totalExpense > 0)
                    {
                        percentage = (amount / totalExpense) * 100;
                    }

                    categorySummaries.Add(new CategorySummary
                    {
                        Name = category.Name,
                        Type = category.Type,
                        Color = category.Color,
                        Amount = amount,
                        Percentage = percentage
                    });
                }
            }

            var dailyData = transactions
                .GroupBy(t => t.Date.Date)
                .Select(g => {
                    var categoriesForDate = g.Select(t => categories.FirstOrDefault(c => c.Id == t.CategoryId));

                    decimal income = g.Where(t => categoriesForDate.Any(c => c != null && c.Id == t.CategoryId && c.Type == "Income"))
                                      .Sum(t => t.Amount);

                    decimal expense = g.Where(t => categoriesForDate.Any(c => c != null && c.Id == t.CategoryId && c.Type == "Expense"))
                                       .Sum(t => t.Amount);

                    return new DailyTransaction
                    {
                        Date = g.Key,
                        Income = income,
                        Expense = expense
                    };
                })
                .OrderBy(d => d.Date)
                .ToList();

            DashboardViewModel viewModel = new DashboardViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = totalIncome - totalExpense,
                StartDate = startDate,
                EndDate = endDate,
                CategorySummaries = categorySummaries.OrderByDescending(c => c.Amount).ToList(),
                DailyTransactions = dailyData
            };

            return View(viewModel);
        }
    }
}