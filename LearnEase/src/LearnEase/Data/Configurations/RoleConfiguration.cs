using LearnEase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnEase.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}