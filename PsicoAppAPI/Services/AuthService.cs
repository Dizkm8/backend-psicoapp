using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class AuthService : IAuthService
    {
        #region CLASS_ATTRIBUTES

        #region INJECTIONS
        private readonly string _jwtSecret;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IMapperService _mapperService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region CONSTANTS
        const string ADMIN_ROLE = "ADMIN";
        const string CLIENT_ROLE = "CLIENT";
        const string SPECIALIST_ROLE = "SPECIALIST";
        #endregion

        #endregion

        #region CLASS_METHODS
        public AuthService(IConfiguration configuration, IUsersUnitOfWork usersUnitOfWork,
            IMapperService mapperService, IHttpContextAccessor httpContextAccessor)
        {
            var token = configuration.GetValue<string>("JwtSettings:Secret") ??
                throw new ArgumentException("JwtSettings:Secret is null");
            _jwtSecret = token;
            _usersUnitOfWork = usersUnitOfWork ?? throw new ArgumentNullException(nameof(usersUnitOfWork));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Token generated or null if the user doesn't exist</returns>
        private async Task<string?> GenerateJwtToken(string userId)
        {
            var user = await _usersUnitOfWork.UserRepository.GetUserById(userId);
            if (user == null) return null;
            var userRole = await GetUserRole(userId);
            if (userRole is null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, userRole)
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
            var client = await _usersUnitOfWork.ClientRepository.GetClientById(userId);
            if (client != null) return client.IsAdministrator ? ADMIN_ROLE : CLIENT_ROLE;
            var specialist = await _usersUnitOfWork.SpecialistRepository.GetSpecialistById(userId);
            return specialist != null ? SPECIALIST_ROLE : null;
        }

        /// <summary>
        /// Get the user id from the token using HttpContext
        /// </summary>
        /// <returns>string with the Id. null if something gone wrong</returns>
        private string? GetUserIdInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser == null) return null;

            //Get Claims from JWT
            var userId = httpUser.FindFirstValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(userId) ? null : userId;
        }

        /// <summary>
        /// Get the user role from the token using HttpContext
        /// </summary>
        /// <returns>string with the Role. null if something gone wrong</returns>
        private string? GetUserRoleInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser == null) return null;

            //Get Claims from JWT
            var userRole = httpUser.FindFirstValue(ClaimTypes.Role);
            return string.IsNullOrEmpty(userRole) ? null : userRole;
        }
        /// <summary>
        /// Get the userId from the token, found a user in Repository and return it
        /// </summary>
        /// <returns>
        /// User found. If the userId or Token are invalid 
        /// or simply user doesn't exists on repository return null
        ///</returns>
        private async Task<User?> GetUserUsingToken()
        {
            var userId = GetUserIdInToken();
            if (userId is null) return null;
            var user = await _usersUnitOfWork.UserRepository.GetUserById(userId);
            return user is not null ? user : null;
        }
        #endregion
    }
}