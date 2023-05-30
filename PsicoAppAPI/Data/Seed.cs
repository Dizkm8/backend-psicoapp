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
            await SeedSpecialities(context, options);
            await SeedSpecialists(context, options);
            await SeedFeedPosts(context, options);
            await SeedForumPosts(context, options);
            await SeedComments(context, options);
            await SeedAppointmentsStatus(context, options);
            await SeedAppointments(context, options);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Seed the database with the appointments in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        /// <returns>Database adding task</returns>
        private static async Task SeedAppointments(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Appointments?.Any();
            if (result == true || result == null) return;
            var appointmentsData = File.ReadAllText("Data/Seeds/AppointmentsData.json");
            var appointmentsList = JsonSerializer.Deserialize<List<Appointment>>(appointmentsData, options);
            if (appointmentsList == null) return;
            await context.Appointments.AddRangeAsync(appointmentsList);
        }

        /// <summary>
        /// Seed the database with the appointments status in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        /// <returns>Database adding task</returns>
        private static async Task SeedAppointmentsStatus(DataContext context, JsonSerializerOptions options)
        {
            var result = context.AppointmentStatuses?.Any();
            if (result == true || result == null) return;
            var appointmentsStatusData = File.ReadAllText("Data/Seeds/AppointmentsStatusData.json");
            var appointmentsStatusList = JsonSerializer.Deserialize<List<AppointmentStatus>>(appointmentsStatusData, options);
            if (appointmentsStatusList == null) return;
            await context.AppointmentStatuses.AddRangeAsync(appointmentsStatusList);
        }

        /// <summary>
        /// Seed the database with the comments in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        /// <returns>Database adding task</returns>
        private static async Task SeedComments(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Comments?.Any();
            if (result == true || result == null) return;
            var commentsData = File.ReadAllText("Data/Seeds/CommentsData.json");
            var commentsList = JsonSerializer.Deserialize<List<Comment>>(commentsData, options);
            if (commentsList == null) return;
            await context.Comments.AddRangeAsync(commentsList);
        }

        /// <summary>
        /// Seed the database with the forum posts in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        /// <returns>Database adding task</returns>
        private static async Task SeedForumPosts(DataContext context, JsonSerializerOptions options)
        {
            var result = context.ForumPosts?.Any();
            if (result == true || result == null) return;
            var forumPostsData = File.ReadAllText("Data/Seeds/ForumPostsData.json");
            var forumPostsList = JsonSerializer.Deserialize<List<ForumPost>>(forumPostsData, options);
            if (forumPostsList == null) return;
            await context.ForumPosts.AddRangeAsync(forumPostsList);
        }

        private static async Task SeedFeedPosts(DataContext context, JsonSerializerOptions options)
        {
            var result = context.FeedPosts?.Any();
            if (result == true || result == null) return;
            var feedPostsData = File.ReadAllText("Data/Seeds/FeedPostsData.json");
            var feedPostsList = JsonSerializer.Deserialize<List<FeedPost>>(feedPostsData, options);
            if (feedPostsList == null) return;
            await context.FeedPosts.AddRangeAsync(feedPostsList);
        }

        /// <summary>
        /// Seed the database with the specialists in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        /// <returns>Database adding task</returns>
        private static async Task SeedSpecialists(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Specialists?.Any();
            if (result == true || result == null) return;
            var specialistsData = File.ReadAllText("Data/Seeds/SpecialistsData.json");
            var specialistsList = JsonSerializer.Deserialize<List<Specialist>>(specialistsData, options);
            if (specialistsList == null) return;
            await context.Specialists.AddRangeAsync(specialistsList);
        }

        /// <summary>
        /// Seed the database with the specialities in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        /// <returns>Database adding task</returns>
        private static async Task SeedSpecialities(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Specialities?.Any();
            if (result == true || result == null) return;
            var specialitiesData = File.ReadAllText("Data/Seeds/SpecialitiesData.json");
            var specialitiesList = JsonSerializer.Deserialize<List<Speciality>>(specialitiesData, options);
            if (specialitiesList == null) return;
            await context.Specialities.AddRangeAsync(specialitiesList);
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
            if (clientList == null) return;
            await context.Clients.AddRangeAsync(clientList);
        }

        #endregion
    }
}