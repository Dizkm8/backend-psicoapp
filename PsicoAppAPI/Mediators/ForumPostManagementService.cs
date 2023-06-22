using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class ForumPostManagementService : IForumPostManagementService
{
    private readonly IFeedPostService _feedPostService;
    private readonly IAuthManagementService _authService;
    private readonly IMapperService _mapperService;
    private readonly ITagService _tagService;

    public ForumPostManagementService(IFeedPostService feedPostService, IAuthManagementService authService,
        IMapperService mapperService, ITagService tagService,
        IOpenAIService openAiService)
    {
        _feedPostService = feedPostService ?? throw new ArgumentNullException(nameof(feedPostService));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
    }
}