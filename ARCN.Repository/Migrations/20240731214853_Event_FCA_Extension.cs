using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Event_FCA_Extension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Extension",
                columns: table => new
                {
                    ExtensionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    LocalGovernmentAreaId = table.Column<int>(type: "int", nullable: true),
                    EstablishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Extension", x => x.ExtensionId);
                    table.ForeignKey(
                        name: "FK_tbl_Extension_LocalGovernmentArea_LocalGovernmentAreaId",
                        column: x => x.LocalGovernmentAreaId,
                        principalTable: "LocalGovernmentArea",
                        principalColumn: "LocalGovernmentAreaId");
                    table.ForeignKey(
                        name: "FK_tbl_Extension_tbl_State_StateId",
                        column: x => x.StateId,
                        principalTable: "tbl_State",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK_tbl_Extension_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FCA",
                columns: table => new
                {
                    FCAId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    LocalGovernmentAreaId = table.Column<int>(type: "int", nullable: true),
                    EstablishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FCA", x => x.FCAId);
                    table.ForeignKey(
                        name: "FK_tbl_FCA_LocalGovernmentArea_LocalGovernmentAreaId",
                        column: x => x.LocalGovernmentAreaId,
                        principalTable: "LocalGovernmentArea",
                        principalColumn: "LocalGovernmentAreaId");
                    table.ForeignKey(
                        name: "FK_tbl_FCA_tbl_State_StateId",
                        column: x => x.StateId,
                        principalTable: "tbl_State",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK_tbl_FCA_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Extension_LocalGovernmentAreaId",
                table: "tbl_Extension",
                column: "LocalGovernmentAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Extension_StateId",
                table: "tbl_Extension",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Extension_UserProfileId",
                table: "tbl_Extension",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FCA_LocalGovernmentAreaId",
                table: "tbl_FCA",
                column: "LocalGovernmentAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FCA_StateId",
                table: "tbl_FCA",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FCA_UserProfileId",
                table: "tbl_FCA",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Extension");

            migrationBuilder.DropTable(
                name: "tbl_FCA");
        }
    }
}
