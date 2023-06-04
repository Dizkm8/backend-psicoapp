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
        private readonly IMapperService _mapperService;

        public UserManagementService(IAuthService authService,
            IUserService userService, IMapperService mapperService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        }

        public async Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto)
        {
            if (registerClientDto is null) return null;
            var user = _mapperService.MapToUser(registerClientDto);
            if (user is null) return null;
            var result = await _userService.AddClient(user);
            return result ? registerClientDto : null;
        }

        public async Task<bool> CheckUserIdAvailability(RegisterClientDto registerClientDto)
        {
            var id = registerClientDto.Id;
            if (string.IsNullOrEmpty(id)) return false;
            var user = await _userService.ExistsUserById(id);
            return user;
        }

        public async Task<bool> CheckEmailAvailability(RegisterClientDto registerClientDto)
        {
            var email = registerClientDto.Email;
            if (string.IsNullOrEmpty(email)) return false;
            var user = await _userService.ExistsUserWithEmail(email);
            return user;
        }

        public async Task<string?> GenerateToken(LoginUserDto loginUserDto)
        {
            var userId = loginUserDto.Id;
            if(string.IsNullOrEmpty(userId)) return null;
            var roleId = await _userService.GetRoleIdInUser(userId);
            return _authService.GenerateToken(userId, roleId.ToString());
        }

        public async Task<bool> CheckCredentials(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Id) ||
                string.IsNullOrEmpty(loginUserDto.Password)) return false;
            var user = await _userService.GetUserByCredentials(loginUserDto.Id, loginUserDto.Password);
            // User need to be enable!
            return user is not null && user.IsEnabled;
        }
    }
}