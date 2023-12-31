using PsicoAppAPI.DTOs.User;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class AdminManagementService : IAdminManagementService
{
    private readonly IAuthManagementService _authService;
    private readonly IOpenAiService _openAiService;
    private readonly IUserService _userService;
    private readonly IMapperService _mapperService;
    private readonly ISpecialistService _specialistService;

    public AdminManagementService(IAuthManagementService authService, IOpenAiService openAiService,
        IUserService userService, IMapperService mapperService, ISpecialistService specialistService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _specialistService = specialistService ?? throw new ArgumentNullException(nameof(specialistService));
    }


    public async Task<string?> GetModerationRules()
    {
        var rules = await _openAiService.GetRules();
        return rules;
    }

    public async Task<bool> SetModerationRules(string newRules)
    {
        var result = await _openAiService.SetRules(newRules);
        return result;
    }

    public async Task<bool> IsUserAdmin()
    {
        var user = await _authService.GetUserEnabledAndAdminFromToken();
        return user is not null;
    }

    public async Task<bool> CheckEmailAvailability(RegisterSpecialistDto specialistDto)
    {
        var email = specialistDto.Email;
        var user = await _userService.ExistsUserWithEmail(email);
        return user;
    }

    public async Task<bool> CheckUserIdAvailability(RegisterSpecialistDto specialistDto)
    {
        var id = specialistDto.Id;
        var user = await _userService.ExistsUserById(id);
        return user;
    }

    public async Task<bool> AddSpecialist(RegisterSpecialistDto specialistDto)
    {
        var user = _mapperService.MapToUser(specialistDto);
        if (user is null) return false;
        // Password are not provided in dto, so, need to be mapped
        user.Password = user.Id;

        var result = await _userService.AddSpecialist(user, specialistDto.SpecialityId);
        return result;
    }

    public async Task<bool> ExistsSpeciality(RegisterSpecialistDto specialistDto)
    {
        var result = await _specialistService.CheckSpecialityById(specialistDto.SpecialityId);
        return result;
    }

    public async Task<bool?> UpdateUserAvailability(string userId, bool isEnabled)
    {
        var user = await _userService.GetUserById(userId);
        if (user is null) return null;

        user.IsEnabled = isEnabled;
        var result = await _userService.CheckUpdateUser(user);
        return result;
    }
}