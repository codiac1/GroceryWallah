using GroceryWallah.DataAccessLayer.Models;

namespace GroceryWallah.DTO.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToDTO(this User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            };
        }
        public static User ToModel(this UserDto user)
        {
            return new User
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
