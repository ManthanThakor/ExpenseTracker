using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerMvc.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string Name { get; set; } = "";

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Category type is required.")]
        [RegularExpression("Expense|Income", ErrorMessage = "Category type must be 'Expense' or 'Income'.")]
        public string Type { get; set; } = "Expense";

        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid color code format.")]
        public string Color { get; set; } = "#007bff";

        public string Icon { get; set; } = "";

        [Required]
        public string UserId { get; set; } = "";

        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
