using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class BlogNesLettrJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorEmail = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorPhoneNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Visibility = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UseBanner = table.Column<bool>(type: "bit", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Blog", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_tbl_Blog_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Journals",
                columns: table => new
                {
                    JournalsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorEmail = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorPhoneNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Visibility = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UseBanner = table.Column<bool>(type: "bit", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Journals", x => x.JournalsId);
                    table.ForeignKey(
                        name: "FK_tbl_Journals_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NewsLetter",
                columns: table => new
                {
                    NewsLetterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorEmail = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorPhoneNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Visibility = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UseBanner = table.Column<bool>(type: "bit", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NewsLetter", x => x.NewsLetterId);
                    table.ForeignKey(
                        name: "FK_tbl_NewsLetter_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Reports",
                columns: table => new
                {
                    ReportsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorEmail = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AuthorPhoneNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Visibility = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UseBanner = table.Column<bool>(type: "bit", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Reports", x => x.ReportsId);
                    table.ForeignKey(
                        name: "FK_tbl_Reports_tbl_UsersProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Blog_UserProfileId",
                table: "tbl_Blog",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Journals_UserProfileId",
                table: "tbl_Journals",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_NewsLetter_UserProfileId",
                table: "tbl_NewsLetter",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Reports_UserProfileId",
                table: "tbl_Reports",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Blog");

            migrationBuilder.DropTable(
                name: "tbl_Journals");

            migrationBuilder.DropTable(
                name: "tbl_NewsLetter");

            migrationBuilder.DropTable(
                name: "tbl_Reports");
        }
    }
}
