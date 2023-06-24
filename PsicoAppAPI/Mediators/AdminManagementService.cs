using PsicoAppAPI.DTOs.User;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class AdminManagementService : IAdminManagementService
{
    private readonly IAuthManagementService _authService;
    private readonly IOpenAiService _openAiService;
    private readonly IUserService _userService;

    public AdminManagementService(IAuthManagementService authService, IOpenAiService openAiService,
        IUserService userService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
}