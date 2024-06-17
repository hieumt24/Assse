using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserFeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d434e69b-a3be-4bcb-8fd7-e557d91b6c30"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisable",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsDisable", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("6bca1b1a-a3b9-45e3-a3f2-e6e9dc89b8b4"), "System", new DateTimeOffset(new DateTime(2024, 6, 14, 15, 22, 42, 430, DateTimeKind.Unspecified).AddTicks(4704), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, false, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAEJGKpUCVb6YepEV7579jipUTjSLh2K3H6t4P5j/a54cN8aviKSewnNo1PC93WPYKLQ==", 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6bca1b1a-a3b9-45e3-a3f2-e6e9dc89b8b4"));

            migrationBuilder.DropColumn(
                name: "IsDisable",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("d434e69b-a3be-4bcb-8fd7-e557d91b6c30"), "System", new DateTimeOffset(new DateTime(2024, 6, 13, 10, 19, 55, 451, DateTimeKind.Unspecified).AddTicks(2382), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 1, false, false, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 1, "AQAAAAIAAYagAAAAEGTZbG5gkYBn/V1qevQpg7S8RPQNSTrOTrDeb9eJd5tfbSFCdfqeyKEgCrEouI/+Ew==", 1, "admin" });
        }
    }
}
