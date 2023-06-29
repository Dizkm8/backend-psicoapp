using OpenAI_API;
using PsicoAppAPI.Services.Interfaces;
using DotNetEnv;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Services
{
    public class OpenAiService : IOpenAiService
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

        public async Task<bool> CheckPsychologyContent(Dictionary<string, string> contentMap)
        {
            var rules = await GetRules();
            if (rules is null) return false;

            if (contentMap == null || contentMap.Count == 0)
                return false;

            var queryBuilder = new StringBuilder();

            foreach (var item in contentMap)
            {
                var tag = item.Key;
                var value = item.Value;

                if (string.IsNullOrEmpty(value))
                    return false;

                queryBuilder.Append($"{tag}: '{value}';");
            }

            var query = rules + " " + queryBuilder.ToString();
            var response = await GetRequest(query);
            if (response is null) return false;

            var result = response.ToLower().Contains("true");
            return result;
        }

        public async Task<string?> GetRules()
        {
            _rules ??= await _unitOfWork.GptRulesRepository.GetRules();
            return _rules;
        }

        public async Task<bool> SetRules(string newRules)
        {
            var result = await _unitOfWork.GptRulesRepository.SetRulesAndSaveChanges(new GptRules()
            {
                Id = 1,
                Rules = newRules
            });
            return result;
        }
    }
}