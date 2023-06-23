using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Models;

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
    public Task<IEnumerable<ForumPostDto>?> GetAllPosts();

    /// <summary>
    /// Check if a post exists based on their post Id
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <returns>true if exists. otherwise false</returns>
    public Task<bool> ExistsPost(int postId);

    /// <summary>
    /// Get a forum post by their Id
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <returns>ForumPostDto with the post. null if do not exists</returns>
    public Task<ForumPostDto?> GetPost(int postId);

    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is specialist
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsUserSpecialist();

    /// <summary>
    /// Add a new comment to an existing post identified by their postId
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <param name="content">Content of the comment</param>
    /// <returns>true if could be added. otherwise false</returns>
    public Task<bool> AddComment(int postId, string content);

    /// <summary>
    /// Delete a post Id identified by the provided postId
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <returns>true if could be added. otherwise false</returns>
    public Task<bool> DeletePost(int postId);
    
    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is admin
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsUserAdmin();
}