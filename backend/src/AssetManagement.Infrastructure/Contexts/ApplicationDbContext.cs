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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(p => p.StaffCode)
                .HasComputedColumnSql("CONCAT('SD', RIGHT('0000' + CAST(StaffCodeId AS VARCHAR(4)), 4))");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.StaffCodeId)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            //seed data

            modelBuilder.Entity<Role>()
                .HasData(
                        new Role { Id = 1, Name = "Admin" },
                        new Role { Id = 2, Name = "Staff" }
                );

            //seed admin user
            var user = new User { FirstName = "SuperUser", LastName = "Admin", Location = EnumLocation.HaNoi, IsFirstTimeLogin = false, Username = "admin" };
            user.PasswordHash = _passwordHasher.HashPassword(user, "adminpassword");
            user.CreatedOn = DateTime.Now;
            user.CreatedBy = "System";
            modelBuilder.Entity<User>()
                .HasData(user);
            modelBuilder.Entity<UserRoles>().HasData(
                               new UserRoles { UserId = user.Id, RoleId = 2 }
            );
        }
    }
}