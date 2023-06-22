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
            SeedFirstOrderTables(context, options);
            SeedSecondOrderTales(context, options);
            SeedThirdOrderTables(context, options);
            SeedFourthOrderTables(context, options);
        }

        /// <summary>
        /// Seed the database with the tables that don't depend on other tables.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedFirstOrderTables(DataContext context, JsonSerializerOptions options)
        {
            SeedSpecialities(context, options);
            SeedRoles(context, options);
            SeedTags(context, options);
            SeedAppointmentStatuses(context, options);
            SeedGptRules(context, options);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the tables that depend on other tables.
        /// the dependency tables must not depend on other tables.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedSecondOrderTales(DataContext context, JsonSerializerOptions options)
        {
            SeedUsers(context, options);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the tables that depend on other tables.
        /// the dependency tables should depend on other tables.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedThirdOrderTables(DataContext context, JsonSerializerOptions options)
        {
            SeedSpecialists(context, options);
            SeedFeedPosts(context, options);
            SeedForumPosts(context, options);
            SeedAppointments(context, options);
            context.SaveChanges();
        }

        private static void SeedFourthOrderTables(DataContext context, JsonSerializerOptions options)
        {
            SeedComments(context, options);
            SeedAvailabilitySlots(context, options);
            context.SaveChanges();
        }

        #region SEED_FIRST_ORDER
        /// <summary>
        /// Seed the database with the rules of GPT in the json file if the database is empty.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        private static void SeedGptRules(DataContext context, JsonSerializerOptions options)
        {
            var result = context.GptRules?.Any();
            if (result is true or null) return;
            var rulesData = File.ReadAllText("Data/Seeds/GPTRulesData.json");
            var rulesList = JsonSerializer.Deserialize<List<GptRules>>(rulesData, options);
            if (rulesList == null) return;
            // Probably Specialities table will not be null, but this validation
            // avoids the warning message
            if (context.GptRules == null) throw new Exception("Specialities table is null");
            context.GptRules.AddRange(rulesList);
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
        /// Seed the database with the appointment statuses in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedAppointmentStatuses(DataContext context, JsonSerializerOptions options)
        {
            var result = context.AppointmentStatuses?.Any();
            if (result == true || result == null) return;
            var AppointmentStatusesData = File.ReadAllText("Data/Seeds/AppointmentStatusesData.json");
            var AppointmentStatusesList = JsonSerializer.Deserialize<List<AppointmentStatus>>(AppointmentStatusesData, options);
            if (AppointmentStatusesList == null) return;
            // Probably Appointments table will not be null, but this validation
            // avoids the warning message
            if (context.AppointmentStatuses == null) throw new Exception("AppointmentStatuses table is null");
            context.AppointmentStatuses.AddRange(AppointmentStatusesList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the tags in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedTags(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Tags?.Any();
            if (result == true || result == null) return;
            var tagsData = File.ReadAllText("Data/Seeds/TagsData.json");
            var tagsList = JsonSerializer.Deserialize<List<Tag>>(tagsData, options);
            if (tagsList == null) return;
            // Probably Appointments table will not be null, but this validation
            // avoids the warning message
            if (context.Tags == null) throw new Exception("Tags table is null");
            context.Tags.AddRange(tagsList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the roles in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedRoles(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Roles?.Any();
            if (result == true || result == null) return;
            var rolesData = File.ReadAllText("Data/Seeds/RolesData.json");
            var rolesList = JsonSerializer.Deserialize<List<Role>>(rolesData, options);
            if (rolesList == null) return;
            // Probably Appointments table will not be null, but this validation
            // avoids the warning message
            if (context.Roles == null) throw new Exception("Roles table is null");
            context.Roles.AddRange(rolesList);
            context.SaveChanges();
        }
        #endregion

        #region SEED_SECOND_ORDER
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
            // Hash the password before save itc
            // If this is not done, the password will be saved as plain text
            // and the BCrypt will crash trying to use it as hash
            userList.ForEach(user =>
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = passwordHash;
            });
            if (context.Users == null) throw new Exception("Users table is null");
            context.Users.AddRange(userList);
            context.SaveChanges();
        }
        #endregion

        #region SEED_THIRD_ORDER
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
        /// Seed the database with the feed posts in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
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
        }
        #endregion

        #region SEED_FOURTH_ORDER
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
        /// Seed the database with the availability slots in the json file if the database is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedAvailabilitySlots(DataContext context, JsonSerializerOptions options)
        {
            var result = context.AvailabilitySlots?.Any();
            if (result == true || result == null) return;
            var availabilitySlotsData = File.ReadAllText("Data/Seeds/AvailabilitySlotsData.json");
            var availabilitySlotsList = JsonSerializer.Deserialize<List<AvailabilitySlot>>(availabilitySlotsData, options);
            if (availabilitySlotsList == null) return;
            // Probably Comments table will not be null, but this validation
            // avoids the warning message
            if (context.AvailabilitySlots == null) throw new Exception("AvailabilitySlots table is null");
            context.AvailabilitySlots.AddRange(availabilitySlotsList);
            context.SaveChanges();
        }
        #endregion

        #endregion
    }

}