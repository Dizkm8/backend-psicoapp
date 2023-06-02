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
        private readonly IMapperService _mapperService;

        public UserManagementService(IUserService userService, IAuthService authService,
            IBCryptService bcryptService, IMapperService mapperService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _bcryptService = bcryptService ?? throw new ArgumentNullException(nameof(bcryptService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        }

        public IUserService UserService => _userService;

        public IAuthService AuthService => _authService;

        public async Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto)
        {
            if (registerClientDto.Password is null) return null;
            var user = _mapperService.MapToUser(registerClientDto);
            if (user is null) return null;
            var result = await _userService.AddClient(user, _bcryptService);
            return result ? registerClientDto : null;
        }

        public async Task<bool> CheckUsersPasswordUsingToken(string? password)
        {
            var result = await _authService.CheckUsersPasswordUsingToken(password, _bcryptService);
            return result;
        }

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
            var profileInfo = await _userService.GetUserProfileInformation(userId, userRole, _mapperService);
            return profileInfo;
        }

        public async Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser)
        {
            var userId = _authService.GetUserIdInToken();
            var userProfile = await _userService.UpdateProfileInformation(newUser, userId, _mapperService);
            return userProfile;
        }

        public async Task<bool> UpdateUserPassword(string? newPassword)
        {
            var userId = _authService.GetUserIdInToken();
            var result = await _userService.UpdateUserPassword(userId, newPassword, _bcryptService);
            return result;
        }
    }
}