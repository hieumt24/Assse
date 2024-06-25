using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entitiesassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Prefix",
                table: "Categories");

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

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("0c8a89b6-a6c9-419d-86ea-830032a2e798"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" },
                    { new Guid("33d69773-05f8-49fc-b18e-a35fc3c6db8d"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" },
                    { new Guid("93e3af38-aab8-417a-abb4-c5503b76673f"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("741f5b0d-5266-4830-8755-ceb307f59b37"), "System", new DateTimeOffset(new DateTime(2024, 6, 24, 17, 0, 10, 906, DateTimeKind.Unspecified).AddTicks(3975), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEEV3ixIzxezznoPHOiKfqfJiZ4r9Sx1t56yxHoEnfHBhD2nMoap6TxUpUyf1kxGmDg==", 1, "adminHN" },
                    { new Guid("bfe13da2-6838-4549-9829-51662433ba28"), "System", new DateTimeOffset(new DateTime(2024, 6, 24, 17, 0, 11, 24, DateTimeKind.Unspecified).AddTicks(62), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEE07T/r1fHQ+SC7PvDmAi2rEo+TV8LeFvEnQkKkxIMpU7+F0tBB1lIuiIQcnWrkEtg==", 1, "adminDN" },
                    { new Guid("f9f979df-60ea-448e-b790-5591b89ae99b"), "System", new DateTimeOffset(new DateTime(2024, 6, 24, 17, 0, 10, 965, DateTimeKind.Unspecified).AddTicks(1249), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEHh+MI1ptY3GXa0v0CaTcTELNEg6v8euVxGqra/hqOGLeT2A1oQ8vvPMXOlhiBjQjw==", 1, "adminHCM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0c8a89b6-a6c9-419d-86ea-830032a2e798"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33d69773-05f8-49fc-b18e-a35fc3c6db8d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("93e3af38-aab8-417a-abb4-c5503b76673f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("741f5b0d-5266-4830-8755-ceb307f59b37"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfe13da2-6838-4549-9829-51662433ba28"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9f979df-60ea-448e-b790-5591b89ae99b"));

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Prefix",
                table: "Categories",
                column: "Prefix",
                unique: true);
        }
    }
}
