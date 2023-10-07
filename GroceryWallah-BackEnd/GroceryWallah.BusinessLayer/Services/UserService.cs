using AutoMapper;
using GroceryWallah.BusinessLayer.IServices;
using GroceryWallah.DataAccessLayer.IRepository;
using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DTO;
using GroceryWallah.DTO.Mapper;
using Microsoft.AspNetCore.Identity;

namespace GroceryWallah.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserDto> _passwordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher<UserDto> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var allUsers = await _userRepository.GetAllUsers();
            var userDtos = new List<UserDto>();

            foreach (var user in allUsers)
            {
                var userDto = UserMapper.ToDTO(user);
                userDtos.Add(userDto);
            }
            return userDtos;
        }
        public async Task<UserDto> Signup(UserDto userDto)
        {
            userDto.UserId = new Guid();
            // Map the UserDto to UserModel
            var userModel = UserMapper.ToModel(userDto);
            userModel.Password = _passwordHasher.HashPassword(userDto, userDto.Password);

            var createdUser = await _userRepository.Signup(userModel);

            var createdUserDto = UserMapper.ToDTO(createdUser);
            return createdUserDto;
        }

        public async Task<UserDto?> Login(string email, string password)
        {
            var loggedInUser = await _userRepository.GetUserByEmail(email);

            if (loggedInUser == null) return null;
            // Map the UserModel to UserDto

            var loggedInUserDto = UserMapper.ToDTO(loggedInUser);
           return VerifyPassword(password, loggedInUserDto);

        }

        private UserDto? VerifyPassword(string enteredPassword, UserDto loggedInUserDto)
        {
            var result = _passwordHasher.VerifyHashedPassword(loggedInUserDto, loggedInUserDto.Password, enteredPassword);
            if (result == PasswordVerificationResult.Success)
            {
                return loggedInUserDto;
            }
            return null;
        }
    }
}