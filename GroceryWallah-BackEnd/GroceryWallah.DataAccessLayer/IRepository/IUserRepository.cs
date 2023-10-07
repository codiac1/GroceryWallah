using GroceryWallah.DataAccessLayer.Models;

namespace GroceryWallah.DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        Task<User> Signup(User user);
       // Task<User> Login(string email, string password);
        Task<User> GetUserByEmail(string email);

        //bool VerifyPassword(string enteredPassword, string storedPassword);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
