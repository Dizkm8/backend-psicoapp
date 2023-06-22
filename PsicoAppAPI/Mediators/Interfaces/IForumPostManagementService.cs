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
    /// <summary>
    /// Using OpenAI API to check if the post is valid
    /// </summary>
    /// <param name="post">FeedpostDto shape with content to check</param>
    /// <returns>True if its adecuate to application. otherwise false</returns>
    public Task<bool> CheckPost(AddForumPostDto post);
    /// <summary>
    /// Get all forum posts 
    /// </summary>
    /// <returns>IEnumerable with the forum posts shaped as Dto</returns>
    public Task<IEnumerable<ForumPostDto>> GetAllPosts();
}