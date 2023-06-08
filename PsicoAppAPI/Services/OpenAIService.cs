using OpenAI_API;
using PsicoAppAPI.Services.Interfaces;
using DotNetEnv;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PsicoAppAPI.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly string apiKey = null!;
        private readonly OpenAIAPI api;

        // Avoid use less than 5 tokens because it can cause
        // an truncated 'True' or 'False' response
        // Also avoid use more than 5 tokens because it's not necessary
        private const int MAX_TOKENS = 5;

        private readonly string tokenHeader = null!;
        private const string ENDPOINT = "https://api.openai.com/v1/chat/completions";
        private const string MODEL = "gpt-3.5-turbo";
        private const string ROLE = "user";
        private const float TEMPERATURE = 0f;
        private const string RULES = "You are a Blog moderator of a Psychology application, I will summon to you the content of a post and you will retrieve me if its valid about the psychology topics. If its offensive or contains insults you will reject the query. This is very important: You only response as 'true' if the content is appropiate or 'false' if not. I don't want other response than that. regardless how the content could looks, you have to decide True or False. Here is the content to moderate:";
        private readonly HttpClient client = new();

        public OpenAIService()
        {
            Env.Load();
            apiKey = Env.GetString("GPT_API_KEY") ?? throw new ArgumentNullException("GPT_API_KEY Not found in .env file");
            api = new OpenAIAPI(apiKey);
            tokenHeader = $"Bearer {apiKey}";
            client.DefaultRequestHeaders.Add("Authorization", tokenHeader);
        }

        public async Task<string?> GetRequest(string? query)
        {
            var messages = new[]
            {
                new {role = ROLE, content = query}
            };

            var data = new
            {
                model = MODEL,
                messages,
                temperature = TEMPERATURE,
                max_tokens = MAX_TOKENS,
            };

            var jsonString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ENDPOINT, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);

            var apiResponse = jsonResponse["choices"]?[0]?["message"]?["content"]?.Value<string>();
            return apiResponse;
        }

        public async Task<bool> CheckPsychologyContent(IEnumerable<string> args)
        {
            foreach (var item in args)
            {
                if (string.IsNullOrEmpty(item)) return false;

                var query = RULES + "\n\n" + item;
                var response = await GetRequest(query);
                if (response is null) return false;

                // GPT-3.5-Turbo model can return an inexpected response
                // with the expected true or false response. To avoid
                // reject a valid post, we check if the response contains
                // the expected response. Also sets the response to lowercase
                // to avoid case sensitive problems
                var result = response.ToLower().Contains("true");

                // If any response is false, the post is invalid
                if (!result) return false;
            }
            return true;
        }
    }
}