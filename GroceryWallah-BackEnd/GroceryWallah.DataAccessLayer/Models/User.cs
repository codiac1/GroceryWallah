using System.ComponentModel.DataAnnotations;

namespace GroceryWallah.DataAccessLayer.Models
{ 
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string Phone { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }

}
