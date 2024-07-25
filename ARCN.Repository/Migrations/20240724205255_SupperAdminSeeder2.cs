using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SupperAdminSeeder2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tbl_Roles");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "tbl_UsersProfile",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "tbl_UsersProfile",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tbl_Roles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
