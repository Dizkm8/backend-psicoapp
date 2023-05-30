using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class DataContext : DbContext
    {
        #region CLASS_ATTRIBUTES
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Specialist> Specialists { get; set; } = null!;
        public DbSet<Speciality> Specialities { get; set; } = null!;
        public DbSet<FeedPost> FeedPosts { get; set; } = null!;
        public DbSet<ForumPost> ForumPosts { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; } = null!;
        #endregion

        #region CLASS_METHODS
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

    }
}