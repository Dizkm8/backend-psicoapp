using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserService _userService;
        private readonly IMapperService _mapperService;
        private readonly IAuthManagementService _authService;

        public UserManagementService(IUserService userService, IMapperService mapperService,
            IAuthManagementService authService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto)
        {
            var user = _mapperService.MapToUser(registerClientDto);
            if(user is null) return null;
            var result = await _userService.AddClient(user);
            return result ? registerClientDto : null;
        }

        public async Task<bool> CheckUserIdAvailability(RegisterClientDto registerClientDto)
        {
            var id = registerClientDto.Id;
            if(string.IsNullOrEmpty(id)) return false;
            var user = await _userService.ExistsUserById(id);
            return user;
        }

        public async Task<bool> CheckEmailAvailability(RegisterClientDto registerClientDto)
        {
            var email = registerClientDto.Email;
            if(string.IsNullOrEmpty(email)) return false;
            var user = await _userService.ExistsUserWithEmail(email);
            return user;
        }

        public async Task<string?> GenerateToken(LoginUserDto loginUserDto)
        {
            var userId = loginUserDto.Id;
            if(string.IsNullOrEmpty(userId)) return null;
            //TODO: Fix order of roleId and getUser here
            var roleId = await _userService.GetRoleIdInUser(userId);
            var user = await _userService.GetUserById(userId);
            if(user is null) return null;
            var userFullName = $"{user.Name} {user.FirstLastName} {user.SecondLastName}";

            return _authService.GenerateToken(userId, roleId.ToString(), userFullName);
        }

        public async Task<bool> CheckCredentials(LoginUserDto loginUserDto)
        {
            if(string.IsNullOrEmpty(loginUserDto.Id) ||
               string.IsNullOrEmpty(loginUserDto.Password)) return false;
            var user = await _userService.GetUserByCredentials(loginUserDto.Id, loginUserDto.Password);
            return user is not null;
        }

        public async Task<bool> CheckUserInToken()
        {
            var userId = _authService.GetUserIdInToken();
            if(string.IsNullOrEmpty(userId)) return false;
            var user = await _userService.GetUserById(userId);
            return user is not null && user.IsEnabled;
        }

        public async Task<bool> CheckUserCurrentPassword(UpdatePasswordDto updatePasswordDto)
        {
            var currentPassword = updatePasswordDto.CurrentPassword;
            if(string.IsNullOrEmpty(currentPassword)) return false;

            var userId = _authService.GetUserIdInToken();
            if(string.IsNullOrEmpty(userId)) return false;

            var user = await _userService.GetUserByCredentials(userId, currentPassword);
            return user is not null;
        }

        public async Task<bool> UpdateUserPassword(UpdatePasswordDto updatePasswordDto)
        {
            var userId = _authService.GetUserIdInToken();
            var password = updatePasswordDto.NewPassword;
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password)) return false;

            var result = await _userService.UpdateUserPassword(userId, password);
            return result;
        }

        public async Task<ProfileInformationDto?> GetUserProfileInformation()
        {
            var user = await _authService.GetUserEnabledFromToken();
            if(user is null) return null;

            var profileInfoDto = _mapperService.MapToProfileInformationDto(user);
            return profileInfoDto;
        }

        public async Task<bool> CheckEmailUpdatingAvailability(UpdateProfileInformationDto dto)
        {
            var userId = _authService.GetUserIdInToken();
            var email = dto.Email;
            if(string.IsNullOrEmpty(email)) return false;

            var result = await _userService.ExistsEmailInOtherUser(email, userId);
            return result;
        }

        public async Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser)
        {
            var user = await _authService.GetUserEnabledFromToken();
            if(user is null) return null;

            var mappedUser = _mapperService.MapAttributesToUser(newUser, user);
            var result = _userService.UpdateUser(mappedUser);
            if(!result) return null;
            return _mapperService.MapToUpdatedProfileInformationDto(mappedUser);
        }

        public async Task<bool> CheckUserEnabled(LoginUserDto loginUserDto)
        {
            var userId = loginUserDto.Id;
            if(string.IsNullOrEmpty(userId)) return false;

            var user = await _userService.GetUserById(userId);
            // User cannot be null and need to be enabled
            return user is not null && user.IsEnabled;
        }
        public async Task<bool> IsUserSpecialist(string userId)
        {
            var user = await GetUserEnabled(userId);
            if(user is null) return false;
            
            var specialistRoleId = await _userService.GetIdOfSpecialistRole();
            return user.RoleId == specialistRoleId;
        }
        public async Task<User?> GetUserEnabled(string userId)
        {
            var user = await _userService.GetUserById(userId);
            if(user is not null && user.IsEnabled) return user;
            return null;
        }
    }
}
