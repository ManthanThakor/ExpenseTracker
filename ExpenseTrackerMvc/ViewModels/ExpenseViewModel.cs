using ExpenseTrackerMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerMvc.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = "";

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
    }

    public class ExpenseListViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; } = Enumerable.Empty<Expense>();
        public string SearchTerm { get; set; } = "";
        public int? CategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ExpenseDeleteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; } = "";
    }

    public class DashboardViewModel
    {
        public decimal TotalAmount { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeFrame { get; set; } = "month";
        public IEnumerable<object> CategoryDistribution { get; set; } = Enumerable.Empty<object>();
        public IEnumerable<object> MonthlyTrends { get; set; } = Enumerable.Empty<object>();
        public IEnumerable<Expense> RecentExpenses { get; set; } = Enumerable.Empty<Expense>();
    }
}