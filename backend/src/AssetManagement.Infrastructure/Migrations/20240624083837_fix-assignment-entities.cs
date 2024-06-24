using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixassignmententities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("88f9dd90-a29e-47df-9db8-58279de031b9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a429cbf2-12bd-4b56-806d-a4781b1e022f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1ebee64-7ca9-4f93-a8be-7be6203c343b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0ad2f3f8-4540-4056-a926-f1f8aa3e6478"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("490c6270-9ebf-47ba-94f1-64cafca07464"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("afdaa978-f5a3-4152-93c3-b566190450ac"));

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("0d046d01-515c-40f9-9842-a6ca03c82ace"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("2cd520ca-5497-4de8-beb7-a5e562fc5458"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" },
                    { new Guid("ff96cad8-b4c1-47e2-a858-0cfc68a4be96"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("712ddd12-dd3c-4dfe-9dc9-a71383f14257"), "System", new DateTimeOffset(new DateTime(2024, 6, 24, 15, 38, 36, 136, DateTimeKind.Unspecified).AddTicks(2906), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEBnvsZBZQohNzdO5oafptYiV1spBiMxH3V28RBx2ZJJM04gD77MQWyCsqQKqy1u+tQ==", 1, "adminDN" },
                    { new Guid("9a67a80f-56c3-4b8d-8f71-a59aa9eeeb1c"), "System", new DateTimeOffset(new DateTime(2024, 6, 24, 15, 38, 36, 31, DateTimeKind.Unspecified).AddTicks(9881), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEDjvQlJfEBXLy5V8leUJxptsvtSgQVciYI6JgU6EMlYKcem+HcKthlbU3T+1opDe6Q==", 1, "adminHCM" },
                    { new Guid("cca40f40-5da5-4ebd-8115-37e946287f01"), "System", new DateTimeOffset(new DateTime(2024, 6, 24, 15, 38, 35, 932, DateTimeKind.Unspecified).AddTicks(2768), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEDvSGndrHYYvkOoqB6YYcFMhYiOO1o3ri6FmrVZY9RahoXb7exuUjXN/+QHKyF4lDg==", 1, "adminHN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0d046d01-515c-40f9-9842-a6ca03c82ace"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2cd520ca-5497-4de8-beb7-a5e562fc5458"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ff96cad8-b4c1-47e2-a858-0cfc68a4be96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("712ddd12-dd3c-4dfe-9dc9-a71383f14257"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9a67a80f-56c3-4b8d-8f71-a59aa9eeeb1c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cca40f40-5da5-4ebd-8115-37e946287f01"));

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Assignments");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("88f9dd90-a29e-47df-9db8-58279de031b9"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" },
                    { new Guid("a429cbf2-12bd-4b56-806d-a4781b1e022f"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("b1ebee64-7ca9-4f93-a8be-7be6203c343b"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("0ad2f3f8-4540-4056-a926-f1f8aa3e6478"), "System", new DateTimeOffset(new DateTime(2024, 6, 23, 22, 3, 4, 691, DateTimeKind.Unspecified).AddTicks(6117), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEJjjKn/2Ug+H//HmloXwCYabNmHaI77j8azzjJXb03OeFo24e7sgzs7yaY1goTo6fQ==", 1, "adminHN" },
                    { new Guid("490c6270-9ebf-47ba-94f1-64cafca07464"), "System", new DateTimeOffset(new DateTime(2024, 6, 23, 22, 3, 4, 810, DateTimeKind.Unspecified).AddTicks(6907), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEMouieAahK4AHFZklVAZmV8Xk5inynVokQwwYdntvPvvQ/p9RTyFuQ6/JBEzeJO9rw==", 1, "adminDN" },
                    { new Guid("afdaa978-f5a3-4152-93c3-b566190450ac"), "System", new DateTimeOffset(new DateTime(2024, 6, 23, 22, 3, 4, 751, DateTimeKind.Unspecified).AddTicks(4110), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAELlA7iKyKs9QkRxMHzwv2l8dvBHkTo5a60/Kkf1e7nAQXsBll/eYUra1eVTjVkauiQ==", 1, "adminHCM" }
                });
        }
    }
}
