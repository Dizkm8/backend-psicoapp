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
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public IUserService UserService => _userService;

        public IAuthService AuthService => _authService;
    }
}