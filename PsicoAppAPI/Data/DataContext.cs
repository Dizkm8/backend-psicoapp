using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User>? Users { get; set; }

        public DbSet<Client>? Clients { get; set; }

        public DbSet<Specialist>? Specialists { get; set; }

        public DbSet<Speciality>? specialities { get; set; }


        public DbSet<FeedPost>? FeedPosts { get; set; }
        public DbSet<ForumPost>? ForumPosts { get; set; }
        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<Comment>? Comments { get; set; }

        public DbSet<AppointmentStatus>? appointmentStatuses { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

    }
}