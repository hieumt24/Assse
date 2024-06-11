using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffCodeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "StaffCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CONCAT('SD', RIGHT('0000' + CAST(StaffCodeId AS VARCHAR(4)), 4))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffCodeId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "StaffCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CONCAT('SD', RIGHT('0000' + CAST(StaffCodeId AS VARCHAR(4)), 4))");
        }
    }
}
