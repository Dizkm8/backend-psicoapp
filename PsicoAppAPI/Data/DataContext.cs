using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class DataContext : DbContext
    {
        #region CLASS_METHODS
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        #region INDEPENDENT_TABLES
        public DbSet<Speciality> Specialities { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; } = null!;
        #endregion

        /// <summary>
        /// Tables that depend on other tables
        /// It not means that they are tables with only one
        /// dependency, it means the type of their
        /// dependency are a independent table
        /// </summary>
        #region SINGLE_DEPENDENCY_TABLES
        public DbSet<User> Users { get; set; } = null!;
        #endregion

        /// <summary>
        /// Tables that depend on tables that depend on other tables/// </summary>
        /// </summary>
        #region DOUBLE_DEPENDENCY_TABLES
        public DbSet<Specialist> Specialists { get; set; } = null!;
        public DbSet<FeedPost> FeedPosts { get; set; } = null!;
        public DbSet<ForumPost> ForumPosts { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        #endregion

        /// <summary>
        /// Tables that depend on tables that depend on other tables, etc.
        /// </summary>
        #region MULTI_DEPENDENCY_TABLES
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.RequestingUser)
                .WithMany()
                .HasForeignKey(a => a.RequestingUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.RequestedUser)
                .WithMany()
                .HasForeignKey(a => a.RequestedUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}