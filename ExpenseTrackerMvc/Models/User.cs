using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerMvc.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50)]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; } = "";
    }
}
