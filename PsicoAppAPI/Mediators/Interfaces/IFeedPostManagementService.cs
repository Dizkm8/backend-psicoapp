using PsicoAppAPI.DTOs.FeedPost;

namespace PsicoAppAPI.Mediators.Interfaces
{
    public interface IFeedPostManagementService : IPostManagementService
    {
        /// <summary>
        /// Add a new FeedPost to the database
        /// </summary>
        /// <param name="feedPostDto">FeedpostDto shape to add</param>
        /// <returns>True if could be added. otherwise false</returns>
        public Task<FeedPostDto?> AddFeedPost(AddFeedPostDto feedPostDto);

        /// <summary>
        /// Get all feed posts 
        /// </summary>
        /// <returns>IEnumerable with the feed posts shaped as Dto</returns>
        public Task<IEnumerable<FeedPostDto>?> GetAllPosts();

        /// <summary>
        /// Check using the token if the userId match with an enabled user and if it is admin
        /// </summary>
        /// <returns>true if match with the filters. otherwise false</returns>
        public Task<bool> IsUserAdmin();

        /// <summary>
        /// Delete a post Id identified by the provided postId
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>true if could be added. otherwise false</returns>
        public Task<bool> DeletePost(int postId);

        /// <summary>
        /// Check if a post exists based on their post Id
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>true if exists. otherwise false</returns>
        public Task<bool> ExistsPost(int postId);

        /// <summary>
        /// Get a feed post by their id
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>FeedPostDto with the information</returns>
        public Task<FeedPostDto?> GetPostById(int postId);
    }
}