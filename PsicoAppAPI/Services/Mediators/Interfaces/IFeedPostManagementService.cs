using PsicoAppAPI.DTOs.FeedPost;

namespace PsicoAppAPI.Services.Mediators.Interfaces
{
    public interface IFeedPostManagementService
    {
        /// <summary>
        /// Add a new FeedPost to the database
        /// </summary>
        /// <param name="feedPostDto">FeedpostDto shape to add</param>
        /// <returns>True if could be added. otherwise false</returns>
        public Task<FeedPostDto?> AddFeedPost(AddFeedPostDto feedPostDto);
        /// <summary>
        /// Check if the tag exists in the database based on TagId provided
        /// </summary>
        /// <param name="feedPostDto">FeedpostDto shape to add</param>
        /// <returns>True if exists. otherwise false</returns>
        public Task<bool> CheckPostTag(AddFeedPostDto feedPostDto);
        /// <summary>
        /// Using OpenAI API to check if the context of the post is valid
        /// </summary>
        /// <param name="feedPostDto">FeedpostDto shape with content to check</param>
        /// <returns>True if its adecuate to application. otherwise false</returns>
        public Task<bool> CheckPostContext(AddFeedPostDto feedPostDto);
    }
}