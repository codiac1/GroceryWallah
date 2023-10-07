using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DTO;

namespace GroceryWallah.BusinessLayer.IServices
{
    public interface IUserService
    {
        Task<UserDto> Signup(UserDto user);
        //Task<User> GetUserByEmail(string email);
        Task<UserDto> Login(string email, string password);
        //bool VerifyPassword(string enteredPassword, string storedPassword);
        Task<IEnumerable<UserDto>> GetAllUsers();
    }

}
