using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class AdminManagementService : IAdminManagementService
{
    private readonly IAuthManagementService _authService;
    private readonly IOpenAiService _openAiService;

    public AdminManagementService(IAuthManagementService authService, IOpenAiService openAiService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
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
}