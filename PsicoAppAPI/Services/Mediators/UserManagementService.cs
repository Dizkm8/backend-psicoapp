using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;
using PsicoAppAPI.Services.Mediators.Interfaces;

namespace PsicoAppAPI.Services.Mediators
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserManagementService(IAuthService authService,
            IUserService userService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<string?> GenerateToken(string? userId)
        {
            if(string.IsNullOrEmpty(userId)) return null;
            var roleId = await _userService.GetRoleIdInUser(userId);
            return _authService.GenerateToken(userId, roleId.ToString());
        }

        public async Task<User?> GetUser(LoginUserDto loginUserDto)
        {
            var user = await _userService.GetUser(loginUserDto);
            return user; 
        }
    }
}