using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Domain.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        //One to many
        builder
        .HasMany(c => c.Products)
        .WithOne(r => r.Category)
        .HasForeignKey(d => d.CategoryId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

        // builder.Entity<Category>()
        //         .HasMany(c => c.Products)
        //         .WithOne(r => r.Category)
        //         .HasForeignKey(d => d.CategoryId)
        //         .IsRequired()
        //         .OnDelete(DeleteBehavior.Restrict);
    }
}