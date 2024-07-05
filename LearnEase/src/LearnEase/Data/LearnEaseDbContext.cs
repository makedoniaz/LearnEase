using LearnEase.Data.Configurations;
using LearnEase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Data
{
    public class LearnEaseDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Log> Logs { get; set; }

        public LearnEaseDbContext(DbContextOptions<LearnEaseDbContext> options) : base(options)
        {
            
        }
    }
}