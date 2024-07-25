using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SupperAdminSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "tbl_UsersProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "tbl_UsersProfile",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "tbl_UsersProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "tbl_UsersProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "tbl_UsersProfile",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "tbl_UsersProfile");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "tbl_UsersProfile");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "tbl_UsersProfile");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "tbl_UsersProfile");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "tbl_UsersProfile");
        }
    }
}
