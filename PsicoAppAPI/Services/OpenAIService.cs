using OpenAI_API;
using OpenAI_API.Completions;
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
        private const int MAX_TOKENS = 150;
        private readonly string RULES = null!;

        public OpenAIService()
        {
            Env.Load();
            apiKey = Env.GetString("GPT_API_KEY") ?? throw new ArgumentNullException("GPT_API_KEY Not found in .env file");
            api = new OpenAIAPI(apiKey);
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

            var completions = await api.Completions.CreateCompletionAsync(request);

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

        public async Task<string?> GetRequestGptTurbo(string? query)
        {
            var request = "Where is the country Chile";

            var endpoint = "https://api.openai.com/v1/chat/completions";

            var messages = new[]
            {
                new {role = "user", content = request}
            };

            var data = new
            {
                model = "gpt-3.5-turbo",
                messages,
                temperature = 0.7,
            };

            var jsonString = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");

            var response = await client.PostAsync(endpoint, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var jsonResponse = JObject.Parse(responseContent);

            var assistantMessageContent = jsonResponse["choices"][0]["message"]["content"].Value<string>();
            return assistantMessageContent;
        }
    }
}