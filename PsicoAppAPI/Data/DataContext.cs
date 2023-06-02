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
        public DbSet<User> Users { get; set; } = null!;
        #endregion

        #region CLASS_METHODS
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.RequestingUser)
                .WithMany()
                .HasForeignKey(a => a.RequestedUserId)
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