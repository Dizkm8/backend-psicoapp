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
        private const string Endpoint = "https://api.openai.com/v1/chat/completions";
        private const string Model = "gpt-3.5-turbo";
        private const string Role = "user";

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

        /// <summary>
        /// Get a request from OpenAI API
        /// This provides a GPT-3.5-Turbo model response
        /// with max 5 tokens.
        /// The APIKey is stored in the environment variable
        /// If its null the program will throw an exception
        /// </summary>
        /// <param name="query">Query to request</param>
        /// <param name="maxTokens">Max tokens of the response</param>
        /// <param name="temperature">Predictability of GPT</param>
        /// <returns>The response of openAI
        /// return null if cannot connect to gpt or query is null
        /// </returns>
        private async Task<string?> GetRequest(string? query, int maxTokens, float temperature)
        {
            var messages = new[]
            {
                new { role = Role, content = query }
            };

            var data = new
            {
                model = Model,
                messages,
                temperature,
                max_tokens = maxTokens,
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

            var query = rules + " " + queryBuilder;
            const int maxTokens = 5;
            const float temperature = 0f;
            var response = await GetRequest(query, maxTokens, temperature);
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
            var result = await _unitOfWork.GptRulesRepository.SetRulesAndSaveChanges(new GptRules
            {
                Id = 1,
                Rules = newRules
            });
            // Update the rules to avoid request database constantly
            if (result) _rules = newRules;
            return result;
        }

        public async Task<string?> ChatWithGpt(string message)
        {
            const int maxTokens = 200;
            const float temperature = 1f;
            var response = await GetRequest(message, maxTokens, temperature);
            return response;
        }
    }
}