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
        #region CLASS_ATTRIBUTES
        readonly string _jwtSecret;
        readonly IUserRepository _userRepository;
        readonly IClientRepository _clientRepository;
        readonly ISpecialistRepository _specialistRepository;
        const string ADMIN_ROLE = "ADMIN";
        const string USER_ROLE = "USER";
        const string SPECIALIST_ROLE = "SPECIALIST";
        #endregion


        #region IUSERSERVICE_METHODS
        public UserService(IConfiguration configuration, IUserRepository userRepository,
            IClientRepository clientRepository, ISpecialistRepository specialistRepository)
        {
            _specialistRepository = specialistRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            var token = configuration.GetValue<string>("JwtSettings:Secret") ??
                throw new ArgumentException("JwtSettings:Secret is null");
            _jwtSecret = token;
        }

        public async Task<string?> GenerateToken(string? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User identifier cannot be null");
            }
            var token = await GenerateJwtToken(id);
            return token;
        }


        public async Task<User?> GetUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrWhiteSpace(loginUserDto.Id) || string.IsNullOrWhiteSpace(loginUserDto.Password)) return null;
            var user = await _userRepository.GetUserByCredentials(loginUserDto.Id, loginUserDto.Password);
            return user;
        }
        #endregion


        #region CLASS_METHODS
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Token generated or null if the user doesn't exist</returns>
        private async Task<string?> GenerateJwtToken(string userId)
        {
            //Temporary stuff to future use role getter method
            var user = _userRepository.GetUserById(userId).Result;
            if (user == null) return null;
            var userRole = await GetUserRole(userId);
            if (userRole == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, userRole) // NEED TO BE CHANGED!! TEMPORARY HARDCODED
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Get the role of the user based on their Id
        /// </summary>
        /// <returns>Role of the user, null if the user doesn't exists</returns>
        private async Task<string?> GetUserRole(string userId)
        {
            var client = await _clientRepository.GetClientById(userId);
            if (client != null) return client.IsAdministrator ? ADMIN_ROLE : USER_ROLE;
            var specialist = await _specialistRepository.GetSpecialistById(userId);
            return specialist != null ? SPECIALIST_ROLE : null;
        }
        #endregion
    }
}