using LearnEase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnEase.Core.Data.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder) {
        builder
            .HasKey(c => c.Id);
            
        builder
            .Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .Ignore(c => c.Username);
    }
}
