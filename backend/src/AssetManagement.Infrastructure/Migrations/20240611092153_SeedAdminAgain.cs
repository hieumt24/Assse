using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0acccdb-cb2f-4fdc-b0cd-9d853e613cfb"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("fae12d3e-66e1-4351-85bb-904bbfe4acee"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 0, false, false, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 0, "AQAAAAIAAYagAAAAEJw66/sCyKacVL7Z6QB30LxXT+WX1rBfW6ZEmCMDMipQkju+6NebkwlQq8ryGgP0wA==", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fae12d3e-66e1-4351-85bb-904bbfe4acee"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("d0acccdb-cb2f-4fdc-b0cd-9d853e613cfb"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser", 0, false, false, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Admin", 0, "AQAAAAIAAYagAAAAEIK8UhrGsChqDp5baoKSOxhfXd7XWhxGQYFVvoFqZpEDIkQQoJRa8heyM425n/YO5g==", 0, "" });
        }
    }
}
