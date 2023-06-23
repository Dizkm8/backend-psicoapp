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
    }
}