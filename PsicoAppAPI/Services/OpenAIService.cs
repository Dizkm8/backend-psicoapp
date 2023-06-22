using OpenAI_API;
using PsicoAppAPI.Services.Interfaces;
using DotNetEnv;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Services
{
    public class OpenAiService : IOpenAIService
    {
        // Avoid use less than 5 tokens because it can cause
        // an truncated 'True' or 'False' response
        // Also avoid use more than 5 tokens because it's not necessary
        private const int MaxTokens = 5;
        private const string Endpoint = "https://api.openai.com/v1/chat/completions";
        private const string Model = "gpt-3.5-turbo";
        private const string Role = "user";
        private const float Temperature = 0f;

        private readonly HttpClient _client = new();
        private string? _rules;
        private readonly IUnitOfWork _unitOfWork;


        public OpenAiService(IUnitOfWork unitOfWork)
        {
            Env.Load();
            var apiKey = Env.GetString("GPT_API_KEY") ??
                         throw new ArgumentNullException("GPT_API_KEY Not found in .env file");
            var tokenHeader = $"Bearer {apiKey}";
            _client.DefaultRequestHeaders.Add("Authorization", tokenHeader);
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<string?> GetRequest(string? query)
        {
            var messages = new[]
            {
                new { role = Role, content = query }
            };

            var data = new
            {
                model = Model,
                messages,
                temperature = Temperature,
                max_tokens = MaxTokens,
            };

            var jsonString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Endpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);

            var apiResponse = jsonResponse["choices"]?[0]?["message"]?["content"]?.Value<string>();
            return apiResponse;
        }

        public async Task<bool> CheckPsychologyContent(IEnumerable<string> args)
        {
            if (!await GetRules())
            {
                return false;
            }

            foreach (var item in args)
            {
                if (string.IsNullOrEmpty(item)) return false;

                var query = _rules + "\n\n" + item;
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

        /// <summary>
        /// Get the rules to gpt moderation from repository
        /// </summary>
        /// <returns>true if exists and could be assigned to _rules attr. otherwise false</returns>
        private async Task<bool> GetRules()
        {
            _rules ??= await _unitOfWork.GptRulesRepository.GetRules();
            return _rules is not null;
        }
    }
}