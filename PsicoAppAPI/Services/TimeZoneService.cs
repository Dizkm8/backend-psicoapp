using DotNetEnv;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly HttpClient _client = new();
        private readonly string apiKey = null!;

        public TimeZoneService()
        {
            Env.Load();
            apiKey = Env.GetString("TIMEZONE_API_KEY") ?? throw new ArgumentNullException("TIMEZONE_API_KEY Not found in .env file");
        }

        public async Task<DateTime?> ConvertToChileUTC(DateTime dateTime)
        {
            string baseUrl = "https://maps.googleapis.com/maps/api/timezone/json";
            string location = "location=-33.4489,-70.6693";
            string timestamp = $"timestamp={(int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds}";

            string url = $"{baseUrl}?{location}&{timestamp}&key={apiKey}";

            // Make the HTTP GET request
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            string responseContent = await response.Content.ReadAsStringAsync();

            // Parse the JSON response
            dynamic? responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            if (responseObject is null) return null;

            // Get the time zone ID from the API response
            string timeZoneId = responseObject.timeZoneId;

            // Get the corresponding TimeZoneInfo object for the time zone ID
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert the provided UTC DateTime to the local time in Chile
            DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);

            return chileTime;
        }
    }
}