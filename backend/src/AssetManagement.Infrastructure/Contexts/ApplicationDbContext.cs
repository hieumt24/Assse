using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(p => p.StaffCode)
                .HasComputedColumnSql("CONCAT('SD', RIGHT('0000' + CAST(StaffCodeId AS VARCHAR(4)), 4))");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            var user = new User { FirstName = "SuperUser", LastName = "Admin", Role = RoleType.Admin, Location = EnumLocation.HaNoi, IsFirstTimeLogin = false, Username = "admin" };
            user.PasswordHash = _passwordHasher.HashPassword(user, "adminpassword");
            modelBuilder.Entity<User>()
                .HasData(user);
        }
    }
}