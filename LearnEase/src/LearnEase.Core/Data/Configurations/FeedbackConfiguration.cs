using LearnEase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnEase.Core.Data.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder) {
        builder
            .HasKey(f => f.Id);
            
        builder
            .Property(f => f.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);

        builder
            .HasOne(f => f.Course)
            .WithMany()
            .HasForeignKey(f => f.CourseId);
    }
}
