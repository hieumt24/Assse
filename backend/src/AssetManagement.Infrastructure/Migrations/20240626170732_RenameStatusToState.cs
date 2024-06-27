using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameStatusToState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1c4a65e-fe3e-44d9-af38-4e229b6f988a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ca944e99-3db6-4b02-9992-69f037e68002"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ec0d9a63-2dc3-4615-889c-881997826b2d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2075b87c-d9da-4786-b90c-fe1346a6c265"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64ea61ce-dfb4-46c6-8bea-7901a840ec7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77f07b6a-fc98-4bb5-b496-18ec27e547ed"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("472ebdc9-da07-4df4-8b16-e314ea7cb0cf"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" },
                    { new Guid("b3eac621-acec-43ae-9953-1314e4570fa0"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("c6ad458f-cf6d-42b1-b4c3-b3d3533f1586"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("b342693e-3e63-4fec-8059-c97dc1a2e157"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 0, 7, 31, 109, DateTimeKind.Unspecified).AddTicks(7580), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEOkG0/6fdU2X8CudJhZGSZGWOrl+jau9W1hmdm1vv74MzGVnvI+OYAIeK9hwLDp0pw==", 1, "adminHN" },
                    { new Guid("c9916272-d62b-45af-8ee0-c69559d7ea8b"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 0, 7, 31, 278, DateTimeKind.Unspecified).AddTicks(4837), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEATFjyHs2FF+ah8eWFwXY+UyViV3ks3D2RmtBA7HEMz9I562+DndqIEhc81alvAEXQ==", 1, "adminHCM" },
                    { new Guid("d67e3ac2-d546-4bff-97f7-bd2b09d54eb7"), "System", new DateTimeOffset(new DateTime(2024, 6, 27, 0, 7, 31, 467, DateTimeKind.Unspecified).AddTicks(7272), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEJXsN/17rOI3QCpqZMSkrZBvaALNhZhu2+gado4yv0qMEQBzlGj8t5PrCWUZH4KvDQ==", 1, "adminDN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("472ebdc9-da07-4df4-8b16-e314ea7cb0cf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b3eac621-acec-43ae-9953-1314e4570fa0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c6ad458f-cf6d-42b1-b4c3-b3d3533f1586"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b342693e-3e63-4fec-8059-c97dc1a2e157"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9916272-d62b-45af-8ee0-c69559d7ea8b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d67e3ac2-d546-4bff-97f7-bd2b09d54eb7"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Prefix" },
                values: new object[,]
                {
                    { new Guid("c1c4a65e-fe3e-44d9-af38-4e229b6f988a"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("ca944e99-3db6-4b02-9992-69f037e68002"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" },
                    { new Guid("ec0d9a63-2dc3-4615-889c-881997826b2d"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("2075b87c-d9da-4786-b90c-fe1346a6c265"), "System", new DateTimeOffset(new DateTime(2024, 6, 26, 23, 49, 46, 503, DateTimeKind.Unspecified).AddTicks(6537), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEIls4sPkMEEeh8D8jD5IUSWtfP6AazDcnnsx79tT3AP1CKc2Z1hkERhUit9y2yBJ6Q==", 1, "adminHCM" },
                    { new Guid("64ea61ce-dfb4-46c6-8bea-7901a840ec7f"), "System", new DateTimeOffset(new DateTime(2024, 6, 26, 23, 49, 46, 731, DateTimeKind.Unspecified).AddTicks(5343), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEG8O6+45+ANVLjMzmaLsVJZYO6BggmNa4BnNV2qX74gvXhhpYCjNd7mgaPrRrNaQ9A==", 1, "adminDN" },
                    { new Guid("77f07b6a-fc98-4bb5-b496-18ec27e547ed"), "System", new DateTimeOffset(new DateTime(2024, 6, 26, 23, 49, 46, 251, DateTimeKind.Unspecified).AddTicks(4087), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEJpaez/dNPmrxCwp+W2TKiWxjgE5QvDgQhfMmCZTKKxeys0NP2NoybvyhDoOfoOZvg==", 1, "adminHN" }
                });
        }
    }
}
