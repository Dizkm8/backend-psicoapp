using System.Text.Json;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));
            if(context.Users.Any()) return;

            var userData = File.ReadAllText("Data/UserSeedData.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var usersList = JsonSerializer.Deserialize<List<User>>(userData, options);
            await context.Users.AddRangeAsync(usersList);
            await context.SaveChangesAsync();

            
        }
    }
}