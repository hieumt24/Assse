using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("60b8e9b4-7fbb-4ae9-ae7f-842307661519"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8897f88d-e88c-4fed-9aa0-14c615ae67c7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a76030e4-a341-47f2-a40d-c342d0a9ac8f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5c7fcf3a-d8d5-4783-8127-3edea3608f83"));

            migrationBuilder.RenameColumn(
                name: "PreFix",
                table: "Categories",
                newName: "Prefix");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "IsDisable", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("000b91d3-c77b-41ed-90d0-f4a3594b6588"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "LA" },
                    { new Guid("40d36eaf-ee50-4eed-9cf6-e650dd526969"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "DE" },
                    { new Guid("d5f17596-3b19-413b-9a73-ddbdeb5f3e30"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "MO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("c3bacf4e-f89f-4db5-aedf-6a305524c65f"), "System", new DateTimeOffset(new DateTime(2024, 6, 18, 5, 56, 28, 363, DateTimeKind.Unspecified).AddTicks(281), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, false, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAECNgoIld6TN4ppkpU1qLKgD0Ssrr2clk7+UzpBBp0IEKNJBtKYbSXkGATOaA3EMu5g==", 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("000b91d3-c77b-41ed-90d0-f4a3594b6588"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("40d36eaf-ee50-4eed-9cf6-e650dd526969"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d5f17596-3b19-413b-9a73-ddbdeb5f3e30"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3bacf4e-f89f-4db5-aedf-6a305524c65f"));

            migrationBuilder.RenameColumn(
                name: "Prefix",
                table: "Categories",
                newName: "PreFix");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "IsDisable", "LastModifiedBy", "LastModifiedOn", "PreFix" },
                values: new object[,]
                {
                    { new Guid("60b8e9b4-7fbb-4ae9-ae7f-842307661519"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "DE" },
                    { new Guid("8897f88d-e88c-4fed-9aa0-14c615ae67c7"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "MO" },
                    { new Guid("a76030e4-a341-47f2-a40d-c342d0a9ac8f"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "LA" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("5c7fcf3a-d8d5-4783-8127-3edea3608f83"), "System", new DateTimeOffset(new DateTime(2024, 6, 18, 4, 59, 15, 888, DateTimeKind.Unspecified).AddTicks(5301), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, false, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAENz0pK+FWN9FVBJ1IXoq8ZKWmCoRLgo6Ylb2fHgYCXW0nMAM/NkZjYK5lOPv+7dmxg==", 1, "admin" });
        }
    }
}
