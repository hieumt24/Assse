using AssetManagement.Domain.Common.Models;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

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

            //And global query filter

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateIsDeletedFilter(entityType.ClrType));
                }
            }

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

            adminDN.PasswordHash = _passwordHasher.HashPassword(adminDN, "adminpassword");
            adminDN.CreatedOn = DateTime.Now;
            adminDN.CreatedBy = "System";

            modelBuilder.Entity<User>()
                .HasData(adminHN, adminHCM, adminDN);

            modelBuilder.Entity<User>().Property(u => u.StaffCodeId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Category>().HasIndex(c => c.CategoryName).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Prefix).IsUnique();
            // seed category
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryName = "Laptop", Prefix = "LA" },
                new Category { CategoryName = "Monitor", Prefix = "MO" },
                new Category { CategoryName = "Desk", Prefix = "DE" }
            );
        }

        private static LambdaExpression CreateIsDeletedFilter(Type entityType)
        {
            var param = Expression.Parameter(entityType, "e");
            var prop = Expression.Property(param, "IsDeleted");
            var condition = Expression.Equal(prop, Expression.Constant(false));
            return Expression.Lambda(condition, param);
        }
    }
}