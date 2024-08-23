using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgramDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseBanner",
                table: "tbl_ARCNProgram");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "tbl_ARCNProgram",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "PublisherName",
                table: "tbl_ARCNProgram",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "PublishOn",
                table: "tbl_ARCNProgram",
                newName: "EventStartDate");

            migrationBuilder.AddColumn<string>(
                name: "DurationPerDay",
                table: "tbl_ARCNProgram",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEndDate",
                table: "tbl_ARCNProgram",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "tbl_ARCNProgram",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationPerDay",
                table: "tbl_ARCNProgram");

            migrationBuilder.DropColumn(
                name: "EventEndDate",
                table: "tbl_ARCNProgram");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "tbl_ARCNProgram");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "tbl_ARCNProgram",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "EventStartDate",
                table: "tbl_ARCNProgram",
                newName: "PublishOn");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "tbl_ARCNProgram",
                newName: "PublisherName");

            migrationBuilder.AddColumn<bool>(
                name: "UseBanner",
                table: "tbl_ARCNProgram",
                type: "bit",
                nullable: true);
        }
    }
}
