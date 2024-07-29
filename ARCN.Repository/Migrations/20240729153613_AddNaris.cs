using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddNaris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalGovernmentArea",
                columns: table => new
                {
                    LocalGovernmentAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    LocalGovernmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGovernmentArea", x => x.LocalGovernmentAreaId);
                    table.ForeignKey(
                        name: "FK_LocalGovernmentArea_tbl_State_StateId",
                        column: x => x.StateId,
                        principalTable: "tbl_State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CordinationReport",
                columns: table => new
                {
                    CordinationReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorEmail = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CordinationReport", x => x.CordinationReportId);
                    table.ForeignKey(
                        name: "FK_tbl_CordinationReport_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Naris",
                columns: table => new
                {
                    NarisId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_tbl_Naris", x => x.NarisId);
                    table.ForeignKey(
                        name: "FK_tbl_Naris_LocalGovernmentArea_LocalGovernmentAreaId",
                        column: x => x.LocalGovernmentAreaId,
                        principalTable: "LocalGovernmentArea",
                        principalColumn: "LocalGovernmentAreaId");
                    table.ForeignKey(
                        name: "FK_tbl_Naris_tbl_State_StateId",
                        column: x => x.StateId,
                        principalTable: "tbl_State",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK_tbl_Naris_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalGovernmentArea_StateId",
                table: "LocalGovernmentArea",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CordinationReport_UserProfileId",
                table: "tbl_CordinationReport",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Naris_LocalGovernmentAreaId",
                table: "tbl_Naris",
                column: "LocalGovernmentAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Naris_StateId",
                table: "tbl_Naris",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Naris_UserProfileId",
                table: "tbl_Naris",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_CordinationReport");

            migrationBuilder.DropTable(
                name: "tbl_Naris");

            migrationBuilder.DropTable(
                name: "LocalGovernmentArea");
        }
    }
}
