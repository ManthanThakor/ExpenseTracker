using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerMvc.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = "";

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = "Expense"; // Can be "Income" or "Expense"

        [StringLength(7, ErrorMessage = "Invalid color code.")]
        public string Color { get; set; } = "#007bff";

        public string Icon { get; set; } = "";

        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
