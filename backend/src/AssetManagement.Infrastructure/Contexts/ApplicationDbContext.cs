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
            //var user = new User { FirstName = "Admin", LastName = "Ha Noi", Role = RoleType.Admin, Location = EnumLocation.HaNoi, IsFirstTimeLogin = false, Username = "admin" };
            //user.PasswordHash = _passwordHasher.HashPassword(user, "adminpassword");
            //user.CreatedOn = DateTime.Now;
            //user.CreatedBy = "System";
            var adminHN = new User
            {
                FirstName = "Admin",
                LastName = "Ha Noi",
                Role = RoleType.Admin,
                Location = EnumLocation.HaNoi,
                IsFirstTimeLogin = false,
                Username = "adminHN"
            };
            adminHN.PasswordHash = _passwordHasher.HashPassword(adminHN, "adminpassword");
            adminHN.CreatedOn = DateTime.Now;
            adminHN.CreatedBy = "System";

            var adminHCM = new User
            {
                FirstName = "Admin",
                LastName = "Ho Chi Minh",
                Role = RoleType.Admin,
                Location = EnumLocation.HoChiMinh,
                IsFirstTimeLogin = false,
                Username = "adminHCM"
            };
            adminHCM.PasswordHash = _passwordHasher.HashPassword(adminHCM, "adminpassword");
            adminHCM.CreatedOn = DateTime.Now;
            adminHCM.CreatedBy = "System";

            var adminDN = new User
            {
                FirstName = "Admin",
                LastName = "Da Nang",
                Role = RoleType.Admin,
                Location = EnumLocation.DaNang,
                IsFirstTimeLogin = false,
                Username = "adminDN"
            };

            modelBuilder.Entity<User>()
                .HasData(adminHN, adminHCM, adminDN);

            modelBuilder.Entity<User>().Property(u => u.StaffCodeId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // seed category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), CategoryName = "Laptop", Prefix = "LA" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Monitor", Prefix = "MO" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Desk", Prefix = "DE" }
            );
        }
    }
}