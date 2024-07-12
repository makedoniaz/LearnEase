using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnEase.Data.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder) {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}