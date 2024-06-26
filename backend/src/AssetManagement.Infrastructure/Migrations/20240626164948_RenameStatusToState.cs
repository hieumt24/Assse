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
                keyValue: new Guid("5b42906d-c30f-47ef-bbce-1c73eba607d7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80c13e12-2336-436c-8282-c5bea32ad46c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fff4a035-0f7c-42ae-a3ab-c700c7139095"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32b90501-6751-445b-bfc5-4a8ebd9334f2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d32afe2-f7e8-4514-8a82-9e863be20f87"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dcee55e0-3c38-4ba9-b33d-d86aed1db88f"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("5b42906d-c30f-47ef-bbce-1c73eba607d7"), "Desk", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "DE" },
                    { new Guid("80c13e12-2336-436c-8282-c5bea32ad46c"), "Monitor", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "MO" },
                    { new Guid("fff4a035-0f7c-42ae-a3ab-c700c7139095"), "Laptop", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "LA" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "IsFirstTimeLogin", "JoinedDate", "LastModifiedBy", "LastModifiedOn", "LastName", "Location", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("32b90501-6751-445b-bfc5-4a8ebd9334f2"), "System", new DateTimeOffset(new DateTime(2024, 6, 26, 10, 6, 48, 573, DateTimeKind.Unspecified).AddTicks(1732), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ha Noi", 1, "AQAAAAIAAYagAAAAEG2zpC05AwLMl/BK5WlGaBWRxPPCPzMlyGoPVvnOk9Wg1SPb1LXRfmqOwLtcPg6aIw==", 1, "adminHN" },
                    { new Guid("5d32afe2-f7e8-4514-8a82-9e863be20f87"), "System", new DateTimeOffset(new DateTime(2024, 6, 26, 10, 6, 48, 701, DateTimeKind.Unspecified).AddTicks(1141), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Da Nang", 2, "AQAAAAIAAYagAAAAEBiMkjamdjl8qYRdNVftWh81CMEypHg5lKg7NdqtYgIvMmmS/cGhh4VhrCpvD9kxMw==", 1, "adminDN" },
                    { new Guid("dcee55e0-3c38-4ba9-b33d-d86aed1db88f"), "System", new DateTimeOffset(new DateTime(2024, 6, 26, 10, 6, 48, 640, DateTimeKind.Unspecified).AddTicks(5595), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 0, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ho Chi Minh", 3, "AQAAAAIAAYagAAAAEHJP5D6F5V8BbnxKFrVcpwe+mkp9SMwLUq390uPON14PuesO7vSKmU/NW8nV+CVm1w==", 1, "adminHCM" }
                });
        }
    }
}
