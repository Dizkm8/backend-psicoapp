using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class PostManagementService : IPostManagementService
{
    private readonly IFeedPostService _feedPostService;
    private readonly IAuthManagementService _authService;
    private readonly IMapperService _mapperService;
    private readonly ITagService _tagService;

    protected PostManagementService(IFeedPostService feedPostService, IAuthManagementService authService,
        IMapperService mapperService, ITagService tagService)
    {
        _feedPostService = feedPostService ?? throw new ArgumentNullException(nameof(feedPostService));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
    }
    

    public async Task<bool> CheckPostTag(AddFeedPostDto feedPostDto)
    {
        var result = await _tagService.ExistsTagById(feedPostDto.TagId);
        return result;
    }
}