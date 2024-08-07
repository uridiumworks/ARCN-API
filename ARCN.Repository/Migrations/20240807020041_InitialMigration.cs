using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARCN.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_State", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UsersProfile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UsersProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Group = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_RoleClaims_tbl_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "tbl_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UserClaims_tbl_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tbl_UserLogins_tbl_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tbl_UserRoles_tbl_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_UserRoles_tbl_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tbl_UserTokens_tbl_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_tbl_Blog_UserProfileId",
                table: "tbl_Blog",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CordinationReport_UserProfileId",
                table: "tbl_CordinationReport",
                column: "UserProfileId");

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

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Journals_UserProfileId",
                table: "tbl_Journals",
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

            migrationBuilder.CreateIndex(
                name: "IX_tbl_NewsLetter_UserProfileId",
                table: "tbl_NewsLetter",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Reports_UserProfileId",
                table: "tbl_Reports",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RoleClaims_RoleId",
                table: "tbl_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "tbl_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserClaims_UserId",
                table: "tbl_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserLogins_UserId",
                table: "tbl_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserRoles_RoleId",
                table: "tbl_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "tbl_UsersProfile",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "tbl_UsersProfile",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Blog");

            migrationBuilder.DropTable(
                name: "tbl_CordinationReport");

            migrationBuilder.DropTable(
                name: "tbl_Extension");

            migrationBuilder.DropTable(
                name: "tbl_FCA");

            migrationBuilder.DropTable(
                name: "tbl_Journals");

            migrationBuilder.DropTable(
                name: "tbl_Naris");

            migrationBuilder.DropTable(
                name: "tbl_NewsLetter");

            migrationBuilder.DropTable(
                name: "tbl_Reports");

            migrationBuilder.DropTable(
                name: "tbl_RoleClaims");

            migrationBuilder.DropTable(
                name: "tbl_UserClaims");

            migrationBuilder.DropTable(
                name: "tbl_UserLogins");

            migrationBuilder.DropTable(
                name: "tbl_UserRoles");

            migrationBuilder.DropTable(
                name: "tbl_UserTokens");

            migrationBuilder.DropTable(
                name: "LocalGovernmentArea");

            migrationBuilder.DropTable(
                name: "tbl_Roles");

            migrationBuilder.DropTable(
                name: "tbl_UsersProfile");

            migrationBuilder.DropTable(
                name: "tbl_State");
        }
    }
}
