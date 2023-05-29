using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class DataContext : DbContext
    {
        #region CLASS_ATTRIBUTES
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Specialist>? Specialists { get; set; }
        public DbSet<Speciality>? Specialities { get; set; }
        public DbSet<FeedPost>? FeedPosts { get; set; }
        public DbSet<ForumPost>? ForumPosts { get; set; }
        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<AppointmentStatus>? AppointmentStatuses { get; set; }
        #endregion

        #region CLASS_METHODS
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

    }
}