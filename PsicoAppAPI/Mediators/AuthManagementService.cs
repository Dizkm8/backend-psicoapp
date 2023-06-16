using PsicoAppAPI.Mediators.Interfaces;
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
    
    public async Task<bool> ExistsUserAndIsEnabled()
    {
        var userId = _authService.GetUserIdInToken();
        var user = await _userService.GetUserById(userId);
        return user is not null && user.IsEnabled;
    }
    public async Task<bool> ExistsUser()
    {
        var userId = _authService.GetUserIdInToken();
        var user = await _userService.GetUserById(userId);
        return user is not null;
    }
}
