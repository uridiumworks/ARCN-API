using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EventEndTime",
                table: "Event",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EventStartTime",
                table: "Event",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEndTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventStartTime",
                table: "Event");
        }
    }
}
