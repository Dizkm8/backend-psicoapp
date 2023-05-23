using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PsicoAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly string _jwtSecret;

        public UsersController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtSecret = configuration.GetValue<string>("JwtSettings:Secret");
        }

        /// <summary>
        /// Get all users in database context
        /// </summary>
        /// <returns>All users collected</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        /// <summary>
        /// Login: checks if the user exists in the database and if the entered password matches the one registered in the database
        /// </summary>
        /// <returns>user whose login credentials match</returns>
        [AllowAnonymous] // Allows the endpoint to be access without authentification
        [HttpPost("login")]
        public IActionResult Login(string id, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id && x.Password == password);

            if (user == null)
            {
                return Unauthorized(); // Returns 401 code if the credentials do not match
            }

            var token = GenerateJwtToken(user.Id);

            return Ok(new { Token = token }); // Returns JWT toker
        }

        /// <summary>
        /// Add a user in database context if user's id is not registered in the database
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>User saved</returns>
        [HttpPost("sign-up")]
        public IActionResult AddUser(User user)
        {
            if (!UserExists(user.Id))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return Conflict();
            }
        }

        // Rest of the code.

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}