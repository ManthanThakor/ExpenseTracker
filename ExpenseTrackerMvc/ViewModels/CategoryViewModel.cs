using ExpenseTrackerMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerMvc.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = "";

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = "Expense";

        [StringLength(7, ErrorMessage = "Invalid color code.")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6})$", ErrorMessage = "Color must be a valid hex color code.")]
        public string Color { get; set; } = "#007bff";

        public string Icon { get; set; } = "";
    }

    public class CategoryListViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public string SearchTerm { get; set; } = "";
        public string CurrentFilter { get; set; } = "";
        public string CategoryType { get; set; } = "All";
    }

    public class CategoryDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public int ExpensesCount { get; set; }
    }
}
