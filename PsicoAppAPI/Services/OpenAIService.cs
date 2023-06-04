using Newtonsoft.Json.Linq;
using OpenAI_API;
using OpenAI_API.Completions;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly string apiKey = null!;
        private readonly OpenAIAPI openAI;
        private const int MAX_TOKENS = 150;
        private readonly string RULES = null!;

        public OpenAIService()
        {
            apiKey = Environment.GetEnvironmentVariable("OpenAI_API_KEY") ??
                throw new ArgumentNullException("OpenAI_API_KEY");
            openAI = new OpenAIAPI(apiKey);
            RULES = "You are a Blog moderator of a Psychology application, I will summon to you the content of a post and you will retrieve me if its valid about the psychology topics. If its offensive or contains insults you will reject the query. This is very important: You only response as 'true' if the content is appropiate or 'false' if not. I don't want other response than that. Here is the content to moderate:";
        }

        public async Task<string?> GetRequest(string? query)
        {
            if (query is null) return null;

            CompletionRequest request = new()
            {
                Prompt = query,
                Model = OpenAI_API.Models.Model.DavinciText,
                MaxTokens = MAX_TOKENS,
                Temperature = 0.0,
            };

            var completions = await openAI.Completions.CreateCompletionAsync(request);
            
            return completions.Completions[0].Text;
        }

        public async Task<bool> CheckPostContent(string? postContent)
        {
            if (string.IsNullOrEmpty(RULES) || string.IsNullOrEmpty(postContent)) return false;
            var query = RULES + "\n\n" + postContent;
            var response = await GetRequest(query);
            if (response is null) return false;
            // Davinci sometimes give to us more words than we expected, so
            // to avoid awkward responses we check if in somewhere the
            // text the word true or false exists
            var result = response.ToLower().Contains("true");
            return result;
        }
    }
}