using PsicoAppAPI.DTOs.FeedPost;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IPostManagementService
{
    /// <summary>
    /// Check if the tag exists in the database based on TagId provided
    /// </summary>
    /// <param name="feedPostDto">FeedpostDto shape to add</param>
    /// <returns>True if exists. otherwise false</returns>
    public Task<bool> CheckPostTag(AddFeedPostDto feedPostDto);
}