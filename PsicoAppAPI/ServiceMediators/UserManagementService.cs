using PsicoAppAPI.ServiceMediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.ServiceMediators
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserManagementService(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
    }
}