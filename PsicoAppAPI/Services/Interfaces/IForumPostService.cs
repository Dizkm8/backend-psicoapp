using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces;

public interface IForumPostService
{
    /// <summary>
    /// Add a new ForumPost to the database
    /// </summary>
    /// <param name="post">ForumPost to add</param>
    /// <returns>True if could be added. otherwise false</returns>
    public Task<bool> AddForumPost(ForumPost? post);

    /// <summary>
    /// Get all ForumPosts from the database
    /// </summary>
    /// <returns>IEnumerable with the forum posts</returns>
    public Task<List<ForumPost>> GetAllPosts();
    /// <summary>
    /// Get a post by their postId
    /// </summary>
    /// <returns>Post if exists. otherwise null</returns>
    public Task<ForumPost?> GetPostById(int postId);

    /// <summary>
    /// Check if a post exists based on their post Id
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <returns>true if exists. otherwise false</returns>
    public Task<bool> ExistsPost(int postId);
    /// <summary>
    /// Add a new comment to a post identified by their post Id in the comment entity
    /// </summary>
    /// <param name="comment">Comment to add</param>
    /// <returns>true if could be added. otherwise false</returns>
    public Task<bool> AddComment(Comment comment);
    /// <summary>
    /// Delete a post by their postId
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <returns>true if the post could be deleted. otherwise false</returns>
    public Task<bool> DeletePostById(int postId);
}