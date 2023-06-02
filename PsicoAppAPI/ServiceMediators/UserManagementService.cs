using PsicoAppAPI.DTOs;
using PsicoAppAPI.ServiceMediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.ServiceMediators
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IBCryptService _bcryptService;

        public UserManagementService(IUserService userService, IAuthService authService,
            IBCryptService bcryptService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _bcryptService = bcryptService ?? throw new ArgumentNullException(nameof(bcryptService));
        }

        public IUserService UserService => _userService;

        public IAuthService AuthService => _authService;

        public async Task<bool> ExistsEmailInOtherUser(string? email)
        {
            var userId = _authService.GetUserIdInToken();
            var result = await _userService.ExistsEmailInOtherUser(email, userId);
            return result;
        }

        public async Task<bool> ExistsUserByToken()
        {
            var userId = _authService.GetUserIdInToken();
            var user = await _userService.GetUserById(userId);
            return user is not null;
        }

        public async Task<ProfileInformationDto?> GetUserProfileInformation()
        {
            var userId = _authService.GetUserIdInToken();
            var userRole = _authService.GetUserRoleInToken();
            var profileInfo = await _userService.GetUserProfileInformation(userId, userRole);
            return profileInfo;
        }

        public async Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser)
        {
            var userId = _authService.GetUserIdInToken();
            var userProfile = await _userService.UpdateProfileInformation(newUser, userId);
            return userProfile;
        }

        public async Task<bool> UpdateUserPassword(string? newPassword)
        {
            var userId = _authService.GetUserIdInToken();
            var hashedPassword = _bcryptService.HashPassword(newPassword);
            var result = await _userService.UpdateUserPassword(userId, hashedPassword);
            return result;
        }
    }
}