using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seedadminlocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Assets",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "IsDisable", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("53f7300c-c7fe-4674-bdad-bbbc72b23543"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "DE" },
                    { new Guid("95d144bb-02d1-4074-9c2d-98353387fe2e"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "MO" },
                    { new Guid("efa04981-bd0a-4108-a40d-467549a8b503"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, null, null, "LA" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("bba7193a-ee6d-4f77-ac0a-2f7cffba6b10"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, false, false, false, new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Da Nang", 2, "", 1, "adminDN" },
                    { new Guid("ecf74a4f-28db-4b75-a755-e24a76c8cd57"), "System", new DateTimeOffset(new DateTime(2024, 6, 19, 11, 15, 56, 23, DateTimeKind.Unspecified).AddTicks(7949), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, false, false, false, new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEECmLK+ExfYPUF+81Byv6oPxqambgTBC+6fUN9Miv1gUn07njt0OqljH7l1ub3ntsg==", 1, "adminHCM" },
                    { new Guid("fb23c8c6-ebaa-4c1e-8326-81105d43cdde"), "System", new DateTimeOffset(new DateTime(2024, 6, 19, 11, 15, 55, 944, DateTimeKind.Unspecified).AddTicks(9879), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, false, false, false, new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEGf6lK3vgdUExVWP+lzYKSBNtrt+e9tuJR5Rr3nfXJ6qozZNWtyztKq5eWOeVyigiw==", 1, "adminHN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("53f7300c-c7fe-4674-bdad-bbbc72b23543"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("95d144bb-02d1-4074-9c2d-98353387fe2e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("efa04981-bd0a-4108-a40d-467549a8b503"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bba7193a-ee6d-4f77-ac0a-2f7cffba6b10"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ecf74a4f-28db-4b75-a755-e24a76c8cd57"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fb23c8c6-ebaa-4c1e-8326-81105d43cdde"));

            migrationBuilder.AlterColumn<bool>(
                name: "State",
                table: "Assets",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
