using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Repositories;

namespace PsicoAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;

        public UsersController(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtSecret = configuration.GetValue<string>("JwtSettings:Secret");
        }

        /// <summary>
        /// Get all users in database context
        /// </summary>
        /// <returns>All users collected</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetUsers());
        }

        /// <summary>
        /// Checks if the user exists in the database and if the entered password matches the one registered in the database
        /// </summary>
        /// <returns>user whose login credentials match</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModelDto loginModelDto)
        {
            var user = _userRepository.GetUserByCredentials(loginModelDto.Id, loginModelDto.Password);

            if (user == null) return Unauthorized(); // Maybe we could change Unauthorized to NotFound here?
            if (user.Id == null) return NotFound();

            var token = GenerateJwtToken(user.Id);

            return Ok(new { Token = token }); // Return the JWT token in the response
        }

        // /// <summary>
        // /// Add a user in database context if user's id is not registered in the database
        // /// </summary>
        // /// <param name="user">User to add</param>
        // /// <returns>User saved</returns>
        // [HttpPost("sign-up")]
        // public async Task<ActionResult> AddUser(User user)
        // {
        //     var userExists = _userRepository.UserExists(user);
        //     if (userExists) return Conflict();
        //     await _userRepository.AddUserAndSaveChanges(user);
        //     return Ok(user);
        // }

        private string GenerateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}