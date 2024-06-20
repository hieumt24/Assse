using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedisableinbaseentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsDisable",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDisable",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDisable",
                table: "Assets");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("12e152f1-0466-44ad-9105-e6cc907ac3bc"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("c5e91723-f161-4fa2-8ef2-c44de4c79845"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" },
                    { new Guid("c7f20851-1605-407d-b765-3afa0106d6e1"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("1f74aee7-ee35-4596-a402-7dbfc301c114"), "System", new DateTimeOffset(new DateTime(2024, 6, 20, 12, 47, 11, 970, DateTimeKind.Unspecified).AddTicks(399), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAELfggdDzt5afmtL+aX5TyztYlQMSekJC85tHc2kn3XSSQwNUMOYuy97HXOpm69aEQQ==", 1, "adminHN" },
                    { new Guid("6e802fb3-1730-4035-828d-be57b5ebc5d8"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "", 1, "adminDN" },
                    { new Guid("8e42a68f-abce-4fa7-8d63-8aa880a35f96"), "System", new DateTimeOffset(new DateTime(2024, 6, 20, 12, 47, 12, 58, DateTimeKind.Unspecified).AddTicks(7248), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAENSx9wIGsXujDyDDvGK9WCF6CkEp2SVzh1IXEQJ6O1E9ubqqHfN34U1ZDnEXqK3/MA==", 1, "adminHCM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("12e152f1-0466-44ad-9105-e6cc907ac3bc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c5e91723-f161-4fa2-8ef2-c44de4c79845"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c7f20851-1605-407d-b765-3afa0106d6e1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f74aee7-ee35-4596-a402-7dbfc301c114"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e802fb3-1730-4035-828d-be57b5ebc5d8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8e42a68f-abce-4fa7-8d63-8aa880a35f96"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisable",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisable",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisable",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
