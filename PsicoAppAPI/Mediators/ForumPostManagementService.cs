using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class ForumPostManagementService : PostManagementService, IForumPostManagementService
{
    private readonly IAuthManagementService _authService;
    private readonly IMapperService _mapperService;
    private readonly ITagService _tagService;
    private readonly IOpenAIService _openAiService;
    private readonly IForumPostService _forumPostService;

    public ForumPostManagementService(IForumPostService forumPostService, IAuthManagementService authService,
        IMapperService mapperService, ITagService tagService, IOpenAIService openAiService) :
        base(authService, mapperService, tagService)
    {
        _forumPostService = forumPostService ?? throw new ArgumentNullException(nameof(forumPostService));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
        _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
    }

    public async Task<ForumPostDto?> AddForumPost(AddForumPostDto forumPostDto)
    {
        var user = await _authService.GetUserEnabledAndClientFromToken();
        if (user is null) return null;
        var userId = user.Id;

        var forumPost = _mapperService.MapToForumPost(forumPostDto);
        // Update properties not mapped
        forumPost.UserId = userId;
        forumPost.PublishedOn = DateOnly.FromDateTime(DateTime.Now);

        var result = await _forumPostService.AddForumPost(forumPost);
        if (!result) return null;

        var postDto = _mapperService.MapToForumPostDto(forumPost);
        // Map tagId to tagName in postDto
        var tag = await _tagService.GetTagById(forumPost.TagId);
        if (postDto is null || tag is null) return null;
        postDto.Tag = tag.Name;
        return postDto;
    }

    public async Task<bool> CheckPost(AddForumPostDto post)
    {
        var content = post.Content;
        var title = post.Title;
        if (content is null || title is null) return false;
        var result = await _openAiService.CheckPsychologyContent(new List<string> { content, title });
        return result;
    }

    public Task<IEnumerable<ForumPostDto>> GetAllPosts()
    {
        throw new NotImplementedException();
    }
}