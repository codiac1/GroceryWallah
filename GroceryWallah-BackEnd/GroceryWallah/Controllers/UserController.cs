using GroceryWallah.BusinessLayer.IServices;
using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroceryWallah.ApiLayer.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserDto userDto)
        {
            try
            {
                var createdUser = await _userService.Signup(userDto);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/api/users/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDTO loginDTO)
        {
            var user = await _userService.Login(loginDTO.Email, loginDTO.Password);
            if (user == default)
                return BadRequest(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = "63797DF2499152D385A89169CED1DA619F9E4C6CB7ED3F1977233AF24B513DA1";
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Name", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("PhoneNumber", user.Phone),
                    new Claim("isAdmin", user.IsAdmin ? "true" : "false")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            var response = new
            {
                jwt,
                user
            };
            return Ok(response);
        }
    }

}
