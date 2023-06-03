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
        public static void SeedData(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CallEachSeeder(context, options);
        }

        /// <summary>
        /// Centralize the call to each seeder method and save changes.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        public static void CallEachSeeder(DataContext context, JsonSerializerOptions options)
        {
            

            SeedSpecialities(context, options);
            SeedUsers(context, options);
            SeedSpecialists(context, options);
            SeedFeedPosts(context, options);
            SeedForumPosts(context, options);
            SeedComments(context, options);
            SeedAppointmentsStatus(context, options);
            SeedAppointments(context, options);
        }

        /// <summary>
        /// Seed the database with the appointments in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedAppointments(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Appointments?.Any();
            if (result == true || result == null) return;
            var appointmentsData = File.ReadAllText("Data/Seeds/AppointmentsData.json");
            var appointmentsList = JsonSerializer.Deserialize<List<Appointment>>(appointmentsData, options);
            if (appointmentsList == null) return;
            // Probably Appointments table will not be null, but this validation
            // avoids the warning message
            if (context.Appointments == null) throw new Exception("Appointments table is null");
            context.Appointments.AddRange(appointmentsList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the appointments status in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedAppointmentsStatus(DataContext context, JsonSerializerOptions options)
        {
            var result = context.AppointmentStatuses?.Any();
            if (result == true || result == null) return;
            var appointmentsStatusData = File.ReadAllText("Data/Seeds/AppointmentsStatusData.json");
            var appointmentsStatusList = JsonSerializer.Deserialize<List<AppointmentStatus>>(appointmentsStatusData, options);
            if (appointmentsStatusList == null) return;
            // Probably AppointmentStatuses table will not be null, but this validation
            // avoids the warning message
            if (context.AppointmentStatuses == null) throw new Exception("AppointmentStatuses table is null");
            context.AppointmentStatuses.AddRange(appointmentsStatusList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the comments in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedComments(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Comments?.Any();
            if (result == true || result == null) return;
            var commentsData = File.ReadAllText("Data/Seeds/CommentsData.json");
            var commentsList = JsonSerializer.Deserialize<List<Comment>>(commentsData, options);
            if (commentsList == null) return;
            // Probably Comments table will not be null, but this validation
            // avoids the warning message
            if (context.Comments == null) throw new Exception("Comments table is null");
            context.Comments.AddRange(commentsList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the forum posts in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedForumPosts(DataContext context, JsonSerializerOptions options)
        {
            var result = context.ForumPosts?.Any();
            if (result == true || result == null) return;
            var forumPostsData = File.ReadAllText("Data/Seeds/ForumPostsData.json");
            var forumPostsList = JsonSerializer.Deserialize<List<ForumPost>>(forumPostsData, options);
            if (forumPostsList == null) return;
            // Probably ForumPosts table will not be null, but this validation
            // avoids the warning message
            if (context.ForumPosts == null) throw new Exception("ForumPosts table is null");
            context.ForumPosts.AddRange(forumPostsList);
            context.SaveChanges();
        }


        private static void SeedFeedPosts(DataContext context, JsonSerializerOptions options)
        {
            var result = context.FeedPosts?.Any();
            if (result == true || result == null) return;
            var feedPostsData = File.ReadAllText("Data/Seeds/FeedPostsData.json");
            var feedPostsList = JsonSerializer.Deserialize<List<FeedPost>>(feedPostsData, options);
            if (feedPostsList == null) return;
            // Probably FeedPosts table will not be null, but this validation
            // avoids the warning message
            if (context.FeedPosts == null) throw new Exception("FeedPosts table is null");
            context.FeedPosts.AddRange(feedPostsList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the specialists in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedSpecialists(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Specialists?.Any();
            if (result == true || result == null) return;
            var specialistsData = File.ReadAllText("Data/Seeds/SpecialistsData.json");
            var specialistsList = JsonSerializer.Deserialize<List<Specialist>>(specialistsData, options);
            if (specialistsList == null) return;
            // Probably Specialists table will not be null, but this validation
            // avoids the warning message
            if (context.Specialists == null) throw new Exception("Specialists table is null");
            context.Specialists.AddRange(specialistsList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the specialities in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedSpecialities(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Specialities?.Any();
            if (result == true || result == null) return;
            var specialitiesData = File.ReadAllText("Data/Seeds/SpecialitiesData.json");
            var specialitiesList = JsonSerializer.Deserialize<List<Speciality>>(specialitiesData, options);
            if (specialitiesList == null) return;
            // Probably Specialities table will not be null, but this validation
            // avoids the warning message
            if (context.Specialities == null) throw new Exception("Specialities table is null");
            context.Specialities.AddRange(specialitiesList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the users in the json file if the database is empty.
        /// </summary>
        /// <param name="context"> Database Context </param>
        /// <param name="options"> Options to Deserialize json </param>
        private static void SeedUsers(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Users?.Any();
            if (result == true || result == null) return;
            var userData = File.ReadAllText("Data/Seeds/UsersData.json");
            var userList = JsonSerializer.Deserialize<List<User>>(userData, options);
            if (userList == null) return;
            // Hash the password before save it
            // Application must crashes if this step is not done
            userList.ForEach(user =>
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
                user.HashedPassword = passwordHash;
            });
            if(context.Users == null) throw new Exception("Users table is null");
            context.Users.AddRange(userList);
            context.SaveChanges();
        }
        #endregion
    }

}