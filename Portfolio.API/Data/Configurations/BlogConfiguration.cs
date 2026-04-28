using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio_Backend.Models.Entities;

namespace Portfolio_Backend.Data.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("blogs");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Slug)
            .IsRequired()
            .HasMaxLength(220);

        builder.HasIndex(b => b.Slug)
            .IsUnique();

        builder.Property(b => b.CoverImageUrl)
            .IsRequired();

        builder.Property(b => b.ShortDescription)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.Tags)
            .HasMaxLength(300);

        builder.Property(b => b.HtmlContent)
            .IsRequired();

        builder.Property(b => b.IsPublished)
            .HasDefaultValue(false);

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

       
    }
}