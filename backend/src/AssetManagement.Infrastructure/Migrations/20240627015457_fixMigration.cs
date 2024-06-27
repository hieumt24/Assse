using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5f74b19a-519c-4365-9ec1-3aac149e1fb0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e63b7a2-3cb8-469a-a518-07055d4c8f39"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a4679587-9ec7-4a45-88f6-d683134e8fd6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4774f393-527b-42e0-a2ea-40f12239249d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e2f65e36-2fec-40b8-b213-c2d8aead595f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e918fca1-aa93-4af2-b681-dadf825737fa"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("362beaf9-5f88-41a1-9268-2c8bd49c9361"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("e80d6fc8-ad25-450a-a3d1-8669c7f8f527"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" },
                    { new Guid("f1c4e3c0-2f4b-41d5-b851-5d153a78d771"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("3ef2f855-64b8-45d1-a28c-46c67eba5126"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 8, 54, 55, 543, DateTimeKind.Unspecified).AddTicks(424), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEFMyILtDqKpFaF+wazJbJpdbzUBIhGrJTDzks/Ij5EIA8iNW0NUY8tefMLG4NmGK3A==", 1, "adminDN" },
                    { new Guid("b40eadcf-f450-43a6-b6dc-29df26a57d4e"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 8, 54, 55, 278, DateTimeKind.Unspecified).AddTicks(7403), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAENkzsxXz51uz5h3ZhwhjhEWdSt5PAOoWSfh+ZLGFLT1A5OhWufwlHFKY+IfwcWDYbA==", 1, "adminHCM" },
                    { new Guid("c29cf571-d957-47e8-b34f-11a5721df0c2"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 8, 54, 54, 998, DateTimeKind.Unspecified).AddTicks(8958), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAECxcq84Yzc+IMyJum2QQGmpj/UOcfDph59GdMORP5gQdb6+vT8h0JbtJmvFYIz7r4g==", 1, "adminHN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("362beaf9-5f88-41a1-9268-2c8bd49c9361"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e80d6fc8-ad25-450a-a3d1-8669c7f8f527"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f1c4e3c0-2f4b-41d5-b851-5d153a78d771"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3ef2f855-64b8-45d1-a28c-46c67eba5126"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b40eadcf-f450-43a6-b6dc-29df26a57d4e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c29cf571-d957-47e8-b34f-11a5721df0c2"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("5f74b19a-519c-4365-9ec1-3aac149e1fb0"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" },
                    { new Guid("9e63b7a2-3cb8-469a-a518-07055d4c8f39"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" },
                    { new Guid("a4679587-9ec7-4a45-88f6-d683134e8fd6"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("4774f393-527b-42e0-a2ea-40f12239249d"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 8, 53, 41, 165, DateTimeKind.Unspecified).AddTicks(1203), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEDXJosgZBqu2UzTDCmGKzlNSxZ9WQrRHFrXVa+aSa4z6kN9hiWeN6fN4fjc73aAH1Q==", 1, "adminDN" },
                    { new Guid("e2f65e36-2fec-40b8-b213-c2d8aead595f"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 8, 53, 40, 712, DateTimeKind.Unspecified).AddTicks(8024), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEK4wJoGbrFUoiB5m54eHyZx/o78YMwtbkLLeSLXgGllW0u0CLS/DQocmg9wPqCiHXQ==", 1, "adminHN" },
                    { new Guid("e918fca1-aa93-4af2-b681-dadf825737fa"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 8, 53, 40, 937, DateTimeKind.Unspecified).AddTicks(8390), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEIhKNVtfRsxLKQUzedK9PLxB2bE8OM9U2+0rfTW1X9pRXpPMv/wIAXAVgb0aTP/TIw==", 1, "adminHCM" }
                });
        }
    }
}
