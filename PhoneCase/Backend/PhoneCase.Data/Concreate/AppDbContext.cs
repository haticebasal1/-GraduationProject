using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneCase.Data.Concreate.Configs;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfig).Assembly);

        #region Users
        var hasher = new PasswordHasher<User>();
        User adminMember = new User("Defne", "Yalçın", "Bakırköy", "İstanbul", Gender.Female)
        {
            Id = "9309f4e0-4338-4af3-bf2b-fb9b1a2061f7",
            UserName = "testadmin",
            NormalizedUserName = "TESTADMIN",
            Email = "testadmin@example.com",
            NormalizedEmail = "TESTADMIN@EXAMPLE.COM",
            EmailConfirmed = true
        };
        adminMember.PasswordHash = hasher.HashPassword(adminMember, "Qwe123.,");

        User userMember = new User("Alp", "Kaya", "Ataköy", "İstanbul", Gender.Male)
        {
            Id = "7dd21677-49a5-4d13-9006-dc909993c4b9",
            UserName = "testuser",
            NormalizedUserName = "TESTUSER",
            Email = "testuser@example.com",
            NormalizedEmail = "TESTUSER@EXAMPLE.COM",
            EmailConfirmed = true
        };
        userMember.PasswordHash = hasher.HashPassword(userMember, "Qwe123.,");

        builder.Entity<User>().HasData(adminMember, userMember);
        #endregion

        #region Roles
        var adminRole = new IdentityRole
        {
            Id = "030acc04-b2c4-4887-9085-e37fd9f498b7",
            Name = "Admin",
            NormalizedName = "ADMIN"
        };

        var userRole = new IdentityRole
        {
            Id = "50a60653-cfab-4d55-8db0-0c0328515dd6",
            Name = "User",
            NormalizedName = "USER"
        };

        builder.Entity<IdentityRole>().HasData(adminRole, userRole);
        #endregion

        #region UserRoles
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = adminMember.Id,
                RoleId = adminRole.Id
            },
            new IdentityUserRole<string>
            {
                UserId = userMember.Id,
                RoleId = userRole.Id
            }
        );
        #endregion

        #region Carts/CartItems
        builder.Entity<Cart>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<CartItem>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Cart>().HasData(
            new Cart(adminMember.Id) { Id = 1 },
            new Cart(userMember.Id) { Id = 2 }
        );
        #endregion

        #region Orders / OrderItems
        builder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
        #endregion

        base.OnModelCreating(builder);
    }
}
