using PsicoAppAPI.DTOs.ForumPost;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IForumPostManagementService : IPostManagementService
{
    /// <summary>
    /// Add a new Forum post to the database
    /// </summary>
    /// <param name="forumPostDto">Forum post Dto shape to add</param>
    /// <returns>True if could be added. otherwise false</returns>
    public Task<ForumPostDto?> AddForumPost(AddForumPostDto forumPostDto);
}