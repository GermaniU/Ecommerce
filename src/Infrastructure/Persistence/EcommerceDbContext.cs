using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Domain.Common;

namespace Ecommerce.Infrastructure.Persistence;

public class EcommerceDbContext : IdentityDbContext<User> {
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options){}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
       
       string UserName = "System";

        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>()) {
            switch (entry.State) {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = UserName;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.LastModifiedBy = UserName;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        
        //One to many
        builder.Entity<Category>()
                .HasMany(c=> c.Products)
                .WithOne(r=>r.Category)
                .HasForeignKey(d=>d.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Product>()
                .HasMany(c=>c.Reviews)
                .WithOne(r=>r.Product)
                .HasForeignKey(c=>c.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                
        builder.Entity<ShoppingCart>()
                .HasMany(c=>c.ShoppingCartItems)
                .WithOne(r=>r.ShoppingCart)
                .HasForeignKey(c=>c.ShoppingCartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

          builder.Entity<Product>()
                .HasMany(c=>c.Images)
                .WithOne(r=>r.Product)
                .HasForeignKey(c=>c.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

       builder.Entity<User>().Property(x=> x.Id).HasMaxLength(36);
        builder.Entity<User>().Property(x=> x.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(x=> x.Id).HasMaxLength(36); 
        builder.Entity<IdentityRole>().Property(x=> x.NormalizedName).HasMaxLength(36);
    }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }

    public DbSet<Image>? Images { get; set; }

    public DbSet<Address>? Addresses { get; set; }

    public DbSet<Order>? Orders { get; set; }

    public DbSet<OrderItem>? OrderItems { get; set; }

    public DbSet<Review>? Reviews { get; set; }

    public DbSet<ShoppingCart>? ShoppingCarts { get; set; }

    public DbSet<ShoppingCartItem>? ShoppingCartItems { get; set; }

    public DbSet<Country>? Countries { get; set; }

    public DbSet<OrderAddress>? OrderAddresses { get; set; }

}