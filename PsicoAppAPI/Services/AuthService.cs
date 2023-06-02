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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBCryptService _bcryptService;
        #endregion

        #region CONSTANTS
        const string ADMIN_ROLE = "ADMIN";
        const string CLIENT_ROLE = "CLIENT";
        const string SPECIALIST_ROLE = "SPECIALIST";
        #endregion

        #endregion

        #region CLASS_METHODS
        public AuthService(IConfiguration configuration, IUsersUnitOfWork usersUnitOfWork,
            IHttpContextAccessor httpContextAccessor, IBCryptService bcryptService)
        {
            var token = configuration.GetValue<string>("JwtSettings:Secret") ??
                throw new ArgumentException("JwtSettings:Secret is null");
            _jwtSecret = token;
            _usersUnitOfWork = usersUnitOfWork ?? throw new ArgumentNullException(nameof(usersUnitOfWork));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _bcryptService = bcryptService ?? throw new ArgumentNullException(nameof(bcryptService));
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
        #endregion

        #region INTERFACE_METHODS
        public async Task<User?> GetUserUsingToken()
        {
            var userId = GetUserIdInToken();
            if (userId is null) return null;
            var user = await _usersUnitOfWork.UserRepository.GetUserById(userId);
            return user is not null ? user : null;
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

        public async Task<string?> GetUserRole(string userId)
        {
            var client = await _usersUnitOfWork.ClientRepository.GetClientById(userId);
            if (client != null) return client.IsAdministrator ? ADMIN_ROLE : CLIENT_ROLE;
            var specialist = await _usersUnitOfWork.SpecialistRepository.GetSpecialistById(userId);
            return specialist != null ? SPECIALIST_ROLE : null;
        }

        public string? GetUserIdInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser == null) return null;

            //Get Claims from JWT
            var userId = httpUser.FindFirstValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(userId) ? null : userId;
        }

        public string? GetUserRoleInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContextAccessor.HttpContext?.User;
            if (httpUser == null) return null;

            //Get Claims from JWT
            var userRole = httpUser.FindFirstValue(ClaimTypes.Role);
            return string.IsNullOrEmpty(userRole) ? null : userRole;
        }

        public async Task<bool> CheckUsersPasswordUsingToken(string? password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            var user = await GetUserUsingToken();
            if (user is null) return false;
            return _bcryptService.VerifyPassword(password, user.Password);
        }
        #endregion
    }
}