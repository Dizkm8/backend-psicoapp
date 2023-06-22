using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class PostManagementService : IPostManagementService
{
    private readonly IAuthManagementService _authService;
    private readonly IMapperService _mapperService;
    private readonly ITagService _tagService;

    protected PostManagementService(IAuthManagementService authService, IMapperService mapperService,
        ITagService tagService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
    }

    public async Task<bool> CheckPostTag(AddFeedPostDto feedPostDto)
    {
        var result = await _tagService.ExistsTagById(feedPostDto.TagId);
        return result;
    }

    public async Task<bool> CheckPostTag(AddForumPostDto forumPostDto)
    {
        var result = await _tagService.ExistsTagById(forumPostDto.TagId);
        return result;
    }
}