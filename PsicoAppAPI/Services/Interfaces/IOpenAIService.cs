namespace PsicoAppAPI.Services.Interfaces
{
    public interface IOpenAiService
    {
        /// <summary>
        /// Check if the content provided is valid in psychology context
        /// This is powered using GPT-3.5-Turbo model
        /// </summary>
        /// <param name="contentMap">Args to check</param>
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

        /// <summary>
        /// Send a message to GPT about psychology and get a response
        /// based on rules and their limitations
        /// </summary>
        /// <param name="message">Message to chat</param>
        /// <returns>request. null if cannot connect with GPT</returns>
        public Task<string?> ChatWithGpt(string message);
    }
}