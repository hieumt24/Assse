using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AssetManagement.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(p => p.StaffCode)
                .HasComputedColumnSql("CONCAT('SD', RIGHT('0000' + CAST(StaffCodeId AS VARCHAR(4)), 4))");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // 1-n category-asset
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Assets)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);


            //seed admin user
            var user = new User { FirstName = "SuperUser", LastName = "Admin", Role = RoleType.Admin, Location = EnumLocation.HaNoi, IsFirstTimeLogin = false, Username = "admin" };
            user.PasswordHash = _passwordHasher.HashPassword(user, "adminpassword");
            user.CreatedOn = DateTime.Now;
            user.CreatedBy = "System";
            modelBuilder.Entity<User>()
                .HasData(user);

            modelBuilder.Entity<User>().Property(u => u.StaffCodeId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            // seed category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), CategoryName = "Laptop" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Monitor" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Mouse" }
            );
        }
    }
}