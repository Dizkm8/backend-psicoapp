using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.DTOs.ForumPost;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IPostManagementService
{
    /// <summary>
    /// Check if the tag exists in the database based on TagId provided
    /// </summary>
    /// <param name="feedPostDto">FeedpostDto shape to add</param>
    /// <returns>True if exists. otherwise false</returns>
    public Task<bool> CheckPostTag(AddFeedPostDto feedPostDto);
    /// <summary>
    /// Check if the tag exists in the database based on TagId provided
    /// </summary>
    /// <param name="forumPostDto">ForumPostDto shape to add</param>
    /// <returns>True if exists. otherwise false</returns>
    public Task<bool> CheckPostTag(AddForumPostDto forumPostDto);
}