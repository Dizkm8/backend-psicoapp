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
            // await SeedFeedPosts(context, options);
            // await SeedForumPosts(context, options);
            // await SeedComments(context, options);
            // await SeedAppointments(context, options);
            await context.SaveChangesAsync();
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