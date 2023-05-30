using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class UserService : IUserService
    {
        private readonly string _jwtSecret;
        private readonly IUserRepository _userRepository;
        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            var token = configuration.GetValue<string>("JwtSettings:Secret") ??
                throw new ArgumentException("JwtSettings:Secret is null");
            _jwtSecret = token;
        }

        public string? GenerateToken(string? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User identifier cannot be null");
            }
            return GenerateJwtToken(id);
        }


        public async Task<User?> GetUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrWhiteSpace(loginUserDto.Id) || string.IsNullOrWhiteSpace(loginUserDto.Password)) return null;
            var user = await _userRepository.GetUserByCredentials(loginUserDto.Id, loginUserDto.Password);
            return user;
        }

        private string? GenerateJwtToken(string userId)
        {
            //Temporary stuff to future use role getter method
            var user = _userRepository.GetUserById(userId).Result;
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, "CLIENT") // NEED TO BE CHANGED!! TEMPORARY HARDCODED
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