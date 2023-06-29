namespace PsicoAppAPI.Services.Interfaces
{
    public interface IOpenAiService
    {
        /// <summary>
        /// Get a request from OpenAI API
        /// This provides a GPT-3.5-Turbo model response
        /// with max 5 tokens.
        /// The APIKey is stored in the environment variable
        /// If its null the program will throw an exception
        /// </summary>
        /// <param name="query">Query to request</param>
        /// <returns>The response of openAI
        /// If the query is null it returns null
        /// </returns>
        public Task<string?> GetRequest(string? query);

        /// <summary>
        /// Check if the content provided is valid in psychology context
        /// This is powered using GPT-3.5-Turbo model
        /// </summary>
        /// <param name="args">array with content to check</param>
        /// <returns>True if it's valid. otherwise false</returns>
        public Task<bool> CheckPsychologyContent(Dictionary<string, string> contentMap);

        /// <summary>
        /// Get the rules to gpt moderation from repository
        /// </summary>
        /// <returns>string if rules exists. otherwise false</returns>
        public Task<string?> GetRules();
        /// <summary>
        /// Set the rules to gpt moderation in the repository
        /// </summary>
        /// <param name="newRules">New rules to update</param>
        /// <returns>true if new rules could be setted. otherwise false</returns>
        public Task<bool> SetRules(string newRules);
    }
}