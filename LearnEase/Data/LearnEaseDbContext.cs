using LearnEase.Data.Configurations;
using LearnEase.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Data
{
    public class LearnEaseDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Log> Logs { get; set; }

        public LearnEaseDbContext(DbContextOptions<LearnEaseDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
        }
    }
}