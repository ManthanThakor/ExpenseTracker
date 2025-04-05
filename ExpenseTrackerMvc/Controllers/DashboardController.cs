using ExpenseTrackerMvc.Services.ExpenseServices;
using ExpenseTrackerMvc.Services.CategoryServices;
using ExpenseTrackerMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerMvc.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITransactionService _expenseService;
        private readonly ICategoryService _categoryService;

        public DashboardController(ITransactionService expenseService, ICategoryService categoryService)
        {
            _expenseService = expenseService;
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

        public async Task<IActionResult> Index(string timeFrame = "month")
        {
            int userId = GetCurrentUserId();
            DateTime startDate, endDate = DateTime.Now;

            // Determine date range based on selected time frame
            switch (timeFrame)
            {
                case "week":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "month":
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
                case "quarter":
                    startDate = DateTime.Now.AddMonths(-3);
                    break;
                case "year":
                    startDate = DateTime.Now.AddYears(-1);
                    break;
                default:
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
            }

            // Get expenses for this time period
            var expenses = await _expenseService.GetExpensesByDateRangeAsync(userId, startDate, endDate);
            var totalAmount = expenses.Sum(e => e.Amount);
            var totalTransactions = expenses.Count();
            var averageAmount = totalTransactions > 0 ? totalAmount / totalTransactions : 0;

            // Get category distribution
            var categoryDistribution = await _expenseService.GetExpensesByCategoryGroupAsync(userId, startDate, endDate);

            // Get monthly trends (for the last 6 months)
            var monthlyTrends = await _expenseService.GetMonthlyExpenseTrendsAsync(userId, 6);

            // Get recent expenses (last 5)
            var recentExpenses = expenses.OrderByDescending(e => e.Date).Take(5);

            DashboardViewModel viewModel = new DashboardViewModel
            {
                TotalAmount = totalAmount,
                TotalTransactions = totalTransactions,
                AverageAmount = averageAmount,
                StartDate = startDate,
                EndDate = endDate,
                TimeFrame = timeFrame,
                CategoryDistribution = categoryDistribution,
                MonthlyTrends = monthlyTrends,
                RecentExpenses = recentExpenses
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetChartData(string timeFrame = "month")
        {
            int userId = GetCurrentUserId();
            DateTime startDate, endDate = DateTime.Now;

            // Determine date range based on selected time frame
            switch (timeFrame)
            {
                case "week":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "month":
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
                case "quarter":
                    startDate = DateTime.Now.AddMonths(-3);
                    break;
                case "year":
                    startDate = DateTime.Now.AddYears(-1);
                    break;
                default:
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
            }

            // Get category distribution
            var categoryDistribution = await _expenseService.GetExpensesByCategoryGroupAsync(userId, startDate, endDate);

            // Get monthly trends (for the last 6 months)
            var monthlyTrends = await _expenseService.GetMonthlyExpenseTrendsAsync(userId, 6);

            return Json(new
            {
                categoryData = categoryDistribution,
                trendData = monthlyTrends
            });
        }
    }
}