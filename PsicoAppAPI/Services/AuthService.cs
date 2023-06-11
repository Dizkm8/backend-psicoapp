using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(IConfiguration config, IHttpContextAccessor httpContext)
        {
            var token = config.GetValue<string>("JwtSettings:Secret") ??
                throw new ArgumentException("JwtSettings:Secret is null");
            _jwtSecret = token;
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public string? GenerateToken(string userId, string userRole, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim("user_name", userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string? GetUserIdInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContext.HttpContext?.User;
            if (httpUser is null) return null;

            //Get Claims from JWT
            var userId = httpUser.FindFirstValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(userId) ? null : userId;
        }

        public int GetUserRoleInToken()
        {
            //Check if the HttpContext is available to work with
            var httpUser = _httpContext.HttpContext?.User;
            if (httpUser is null) return -1;

            //Get Claims from JWT
            var userRole = httpUser.FindFirstValue(ClaimTypes.Role);
            if (!Int32.TryParse(userRole, out var roleId)) return -1;
            return roleId;
        }
    }
}