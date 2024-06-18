using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoriesFeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0c1ab665-14b5-4565-8d0c-2a363505f1ae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2423fc96-0941-40c1-a547-a6122662df3c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f3fe76e0-c9d0-491c-a209-6f40a9a98b5b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("78442b86-8abb-4e60-a49b-c4130f19b502"));

            migrationBuilder.AddColumn<string>(
                name: "PreFix",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PreFix",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "IsDisable", "LastModifiedBy", "LastModifiedOn" },
                values: new object[,]
                {
                    { new Guid("0c1ab665-14b5-4565-8d0c-2a363505f1ae"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null },
                    { new Guid("2423fc96-0941-40c1-a547-a6122662df3c"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null },
                    { new Guid("f3fe76e0-c9d0-491c-a209-6f40a9a98b5b"), "Mouse", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("78442b86-8abb-4e60-a49b-c4130f19b502"), "System", new DateTimeOffset(new DateTime(2024, 6, 17, 13, 26, 31, 588, DateTimeKind.Unspecified).AddTicks(735), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, false, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAEKmjyP7bDgo+HT+5mvzZ/Dr6HbIjkA5T/aOcknt+AzHNvPfKgsIocaV6W8CpaOuCvw==", 1, "admin" });
        }
    }
}
