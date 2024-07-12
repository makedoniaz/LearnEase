using LearnEase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnEase.Core.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .Property(c => c.AmountOfLectures)
            .IsRequired();

        builder
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId);
    }
}
