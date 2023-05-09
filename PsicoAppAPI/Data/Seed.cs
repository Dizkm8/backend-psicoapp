using System.Text.Json;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class Seed
    {
        /// <summary>
        /// Seed the database with examples models in the json files if the database is empty.
        /// </summary>
        /// <param name="context">Database Context </param>
        /// <returns>Database save changes Task</returns>
        public static async Task SeedData(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            await SeedUsers(context, options);
            await SeedFeedPost(context, options);
            await context.SaveChangesAsync();
        }
        
        /// <summary>
        /// Seed the database with the users in the json file if the database is empty.
        /// </summary>
        /// <param name="context"> Database Context </param>
        /// <param name="options"> Options to Deserialize json </param>
        /// <returns>Database adding Task</returns>
        private static async Task SeedUsers(DataContext context, JsonSerializerOptions options)
        {
            if (context.Users.Any()) return;
            var userData = File.ReadAllText("Data/Seeds/UserSeedData.json");
            var usersList = JsonSerializer.Deserialize<List<User>>(userData, options);
            await context.Users.AddRangeAsync(usersList);
        }

        /// <summary>
        /// Seed the database with the feed posts in the json file if the database is empty.
        /// </summary>
        /// <param name="context"> Database Context </param>
        /// <param name="options"> Options to Deserialize json </param>
        /// <returns>Database adding Task</returns>
        private static async Task SeedFeedPost(DataContext context, JsonSerializerOptions options)
        {
            if (context.FeedPosts.Any()) return;
            var feedPostsData = File.ReadAllText("Data/Seeds/FeedPostSeedData.json");
            var feedPostsList = JsonSerializer.Deserialize<List<FeedPost>>(feedPostsData, options);
            await context.FeedPosts.AddRangeAsync(feedPostsList);
        }
    }
}