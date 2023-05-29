using System.Text.Json;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class Seed
    {
        #region CLASS_METHODS

        /// <summary>
        /// Seed the database with examples models in the json files if the database is empty.
        /// </summary>
        /// <param name="context">Database Context </param>
        /// <returns>Database save changes Task</returns>
        public static async Task SeedData(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            await SeedClients(context, options);
            // await SeedFeedPosts(context, options);
            // await SeedForumPosts(context, options);
            // await SeedComments(context, options);
            // await SeedAppointments(context, options);
            await context.SaveChangesAsync();
        }

        // /// <summary>
        // /// Seed the database with the clients in the json file if the database is empty.
        // /// </summary>
        // /// <param name="context"> Database Context </param>
        // /// <param name="options"> Options to Deserialize json </param>
        // /// <returns>Database adding Task</returns>
        private static async Task SeedClients(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Clients?.Any();
            if (result == true || result == null) return;
            var clientData = File.ReadAllText("Data/Seeds/ClientsData.json");
            var clientList = JsonSerializer.Deserialize<List<Client>>(clientData, options);
            if(clientList == null) return;
            await context.Clients.AddRangeAsync(clientList);
        }

        // /// <summary>
        // /// Seed the database with the feed posts in the json file if the database is empty.
        // /// </summary>
        // /// <param name="context"> Database Context </param>
        // /// <param name="options"> Options to Deserialize json </param>
        // /// <returns>Database adding Task</returns>
        // private static async Task SeedFeedPosts(DataContext context, JsonSerializerOptions options)
        // {
        //     if (context.FeedPosts.Any()) return;
        //     var feedPostsData = File.ReadAllText("Data/Seeds/FeedPostSeedData.json");
        //     var feedPostsList = JsonSerializer.Deserialize<List<FeedPost>>(feedPostsData, options);
        //     await context.FeedPosts.AddRangeAsync(feedPostsList);
        // }

        // /// <summary>
        // /// Seed the database with the forum posts in the json file if the database is empty.
        // /// </summary>
        // /// <param name="context"> Database Context </param>
        // /// <param name="options"> Options to Deserialize json </param>
        // /// <returns>Database adding Task</returns>
        // private static async Task SeedForumPosts(DataContext context, JsonSerializerOptions options)
        // {
        //     if (context.ForumPosts.Any()) return;
        //     var forumPostsData = File.ReadAllText("Data/Seeds/ForumPostSeedData.json");
        //     var forumPostsList = JsonSerializer.Deserialize<List<ForumPost>>(forumPostsData, options);
        //     await context.ForumPosts.AddRangeAsync(forumPostsList);
        // }

        // /// <summary>
        // /// Seed the database with the comments in the json file if the database is empty.
        // /// </summary>
        // /// <param name="context"> Database Context </param>
        // /// <param name="options"> Options to Deserialize json </param>
        // /// <returns>Database adding Task</returns>
        // private static async Task SeedComments(DataContext context, JsonSerializerOptions options)
        // {
        //     if (context.Comments.Any()) return;
        //     var commentsData = File.ReadAllText("Data/Seeds/CommentSeedData.json");
        //     var commentsList = JsonSerializer.Deserialize<List<Comment>>(commentsData, options);
        //     await context.Comments.AddRangeAsync(commentsList);
        // }

        // /// <summary>
        // /// Seed the database with the appointments in the json file if the database is empty.
        // /// </summary>
        // /// <param name="context"> Database Context </param>
        // /// <param name="options"> Options to Deserialize json </param>
        // /// <returns>Database adding Task</returns>
        // private static async Task SeedAppointments(DataContext context, JsonSerializerOptions options)
        // {
        //     if (context.Appointments.Any()) return;
        //     var appointmentsData = File.ReadAllText("Data/Seeds/AppointmentSeedData.json");
        //     var appointmentsList = JsonSerializer.Deserialize<List<Appointment>>(appointmentsData, options);
        //     await context.Appointments.AddRangeAsync(appointmentsList);
        // }
       
       #endregion
    }
}