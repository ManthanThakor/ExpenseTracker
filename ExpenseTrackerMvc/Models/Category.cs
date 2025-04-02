using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerMvc.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = "";

        [StringLength(200)]
        public string Description { get; set; } = "";

        [Required]
        public string Type { get; set; } = "Expense";

        [StringLength(7)]
        public string Color { get; set; } = "#007bff";

        public string Icon { get; set; } = "";

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
