using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("72652c68-9deb-4378-8e9e-12ea28975eb9"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "IsDisable", "LastModifiedBy", "LastModifiedOn" },
                values: new object[,]
                {
                    { new Guid("1cfecdf4-a74d-42e7-b68d-eae822214397"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null },
                    { new Guid("b62f4360-08e6-4f3f-812d-ea03f447a948"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null },
                    { new Guid("dea718a3-9a0f-473f-926c-9b89098e9a6d"), "Mouse", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("fe44619b-a1b4-4890-8a2c-c64cc65d5f20"), "System", new DateTimeOffset(new DateTime(2024, 6, 17, 12, 35, 33, 883, DateTimeKind.Unspecified).AddTicks(6403), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, false, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAEKrFm7iJqNryNSSescjACvyIOxS3+jRC8LYQrCBH2uAqvJ2+2+31uCGy6t/XJAmhUQ==", 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1cfecdf4-a74d-42e7-b68d-eae822214397"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b62f4360-08e6-4f3f-812d-ea03f447a948"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("dea718a3-9a0f-473f-926c-9b89098e9a6d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fe44619b-a1b4-4890-8a2c-c64cc65d5f20"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("72652c68-9deb-4378-8e9e-12ea28975eb9"), "System", new DateTimeOffset(new DateTime(2024, 6, 17, 11, 18, 15, 110, DateTimeKind.Unspecified).AddTicks(7803), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, false, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAEAGp4jYFkqSAX8aqx9kneYgNkKZ00bPsT6oZhnBhtkBfm4RNI3B/v1WxJXQ29k/Z7w==", 1, "admin" });
        }
    }
}
