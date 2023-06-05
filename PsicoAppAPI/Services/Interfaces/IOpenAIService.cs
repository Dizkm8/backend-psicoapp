namespace PsicoAppAPI.Services.Interfaces
{
    public interface IOpenAIService
    {
        /// <summary>
        /// Get a request from OpenAI API
        /// This provides a DavinciText model response
        /// with max 150 tokens.
        /// The APIKey is stored in the environment variable
        /// If its null the program will throw an exception
        /// </summary>
        /// <param name="query">Query to request</param>
        /// <returns>The response of openAI
        /// If the query is null it returns null
        /// </returns>
        public Task<string?> GetRequest(string? query);

        /// <summary>
        /// Check if the content of the post is valid
        /// using OpenAI API
        /// </summary>
        /// <param name="postContent">Post content body to check</param>
        /// <returns>True if it's valid. otherwise false</returns>
        public Task<bool> CheckPostContent(string? postContent);
    }
}