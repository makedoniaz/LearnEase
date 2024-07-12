using LearnEase.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Core.Data;

public class LearnEaseDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Course> Courses { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }

    public DbSet<Log> Logs { get; set; }

    public LearnEaseDbContext(DbContextOptions<LearnEaseDbContext> options) : base(options)
    {

    }
}
