using OpenAI_API.Moderation;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Mediators.Interfaces;
using PsicoAppAPI.Models;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Mediators;

public class ForumPostManagementService : PostManagementService, IForumPostManagementService
{
    private readonly IAuthManagementService _authService;
    private readonly IMapperService _mapperService;
    private readonly ITagService _tagService;
    private readonly IOpenAiService _openAiService;
    private readonly IForumPostService _forumPostService;

    public ForumPostManagementService(IForumPostService forumPostService, IAuthManagementService authService,
        IMapperService mapperService, ITagService tagService, IOpenAiService openAiService) :
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
        postDto.TagName = tag.Name;
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

    public async Task<IEnumerable<ForumPostDto>?> GetAllPosts()
    {
        var posts = await _forumPostService.GetAllPosts();
        var mappedPosts = _mapperService.MapToForumPostDto(posts);
        return mappedPosts;
    }

    public async Task<bool> ExistsPost(int postId)
    {
        var result = await _forumPostService.ExistsPost(postId);
        return result;
    }

    public async Task<ForumPostDto?> GetPost(int postId)
    {
        var post = await _forumPostService.GetPostById(postId);
        var mappedPost = _mapperService.MapToForumPostDto(post);
        return mappedPost;
    }

    public async Task<bool> IsUserSpecialist()
    {
        var user = await _authService.GetUserEnabledAndSpecialistFromToken();
        return user is not null;
    }

    public async Task<bool> AddComment(int postId, string content)
    {
        var user = await _authService.GetUserEnabledAndSpecialistFromToken();
        if (user is null) return false;

        var comment = new Comment()
        {
            Content = content,
            PublishedOn = DateTime.Now,
            UserId = user.Id,
            ForumPostId = postId
        };

        var result = await _forumPostService.AddComment(comment);
        return result;
    }

    public async Task<bool> DeletePost(int postId)
    {
        var result = await _forumPostService.DeletePostById(postId);
        return result;
    }

    public async Task<bool> IsUserAdmin()
    {
        var user = await _authService.GetUserEnabledAndAdminFromToken();
        return user is not null;
    }

    public async Task<bool> DeleteComment(int postId, int commentId)
    {
        var result = await _forumPostService.DeleteComment(postId, commentId);
        return result;
    }

    public Task<bool> ExistsComment(int postId, int commentId)
    {
        var result = _forumPostService.ExistsComment(postId, commentId);
        return result;
    }
}