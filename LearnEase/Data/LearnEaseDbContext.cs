using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Data.Configurations;
using LearnEase.Models;
using LearnEase.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Data
{
    public class LearnEaseDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

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