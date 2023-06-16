using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class AuthManagementService : IAuthManagementService
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthManagementService(IAuthService authService, IUserService userService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<User?> GetUserEnabledFromToken()
    {
        var userId = _authService.GetUserIdInToken();
        var user = await _userService.GetUserById(userId);
        if(user is not null && user.IsEnabled) return user;
        return null;
    }
    public async Task<bool> ExistsUserInTokenAndIsEnabled()
    {
        var user = await GetUserEnabledFromToken();
        return user is not null;
    }
    public async Task<bool> ExistsUserInToken()
    {
        var userId = _authService.GetUserIdInToken();
        var user = await _userService.GetUserById(userId);
        return user is not null;
    }
    public string? GetUserIdFromToken()
    {
        var userId = _authService.GetUserIdInToken();
        return userId;
    }
    public string? GenerateToken(string userId, string userRole, string userName)
    {
        var token = _authService.GenerateToken(userId, userRole, userName);
        return token;
    }
    public string? GetUserIdInToken()
    {
        var userId = _authService.GetUserIdInToken();
        return userId;
    }
    public int GetUserRoleInToken()
    {
        var userRole = _authService.GetUserRoleInToken();
        return userRole;
    }
    public bool ExistsUserRoleInToken()
    {
        var userRole = GetUserRoleInToken();
        return userRole != -1;
    }
}
