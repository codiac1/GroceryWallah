
namespace GroceryWallah.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
