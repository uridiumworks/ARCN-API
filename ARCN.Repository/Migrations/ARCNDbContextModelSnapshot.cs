﻿// <auto-generated />
using System;
using ARCN.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ARCN.Repository.Migrations
{
    [DbContext(typeof(ARCNDbContext))]
    partial class ARCNDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ARCN.Domain.Entities.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Group")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("tbl_RoleClaims", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("tbl_UsersProfile", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BlogId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"));

                    b.Property<string>("AuthorEmail")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorEmail");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorName");

                    b.Property<string>("AuthorPhoneNumber")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("AuthorPhoneNumber");

                    b.Property<string>("BannerUrl")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("BannerUrl");

                    b.Property<string>("Category")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Category");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PublishDate");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Title");

                    b.Property<bool?>("UseBanner")
                        .HasColumnType("bit")
                        .HasColumnName("UseBanner");

                    b.Property<string>("UserProfileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserProfileId");

                    b.Property<string>("Visibility")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Visibility");

                    b.HasKey("BlogId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("tbl_Blog", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.Journals", b =>
                {
                    b.Property<int>("JournalsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("JournalsId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JournalsId"));

                    b.Property<string>("AuthorEmail")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorEmail");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorName");

                    b.Property<string>("AuthorPhoneNumber")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("AuthorPhoneNumber");

                    b.Property<string>("BannerUrl")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("BannerUrl");

                    b.Property<string>("Category")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Category");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PublishDate");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Title");

                    b.Property<bool?>("UseBanner")
                        .HasColumnType("bit")
                        .HasColumnName("UseBanner");

                    b.Property<string>("UserProfileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserProfileId");

                    b.Property<string>("Visibility")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Visibility");

                    b.HasKey("JournalsId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("tbl_Journals", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.NewsLetter", b =>
                {
                    b.Property<int>("NewsLetterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NewsLetterId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsLetterId"));

                    b.Property<string>("AuthorEmail")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorEmail");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorName");

                    b.Property<string>("AuthorPhoneNumber")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("AuthorPhoneNumber");

                    b.Property<string>("BannerUrl")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("BannerUrl");

                    b.Property<string>("Category")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Category");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PublishDate");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Title");

                    b.Property<bool?>("UseBanner")
                        .HasColumnType("bit")
                        .HasColumnName("UseBanner");

                    b.Property<string>("UserProfileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserProfileId");

                    b.Property<string>("Visibility")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Visibility");

                    b.HasKey("NewsLetterId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("tbl_NewsLetter", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.Reports", b =>
                {
                    b.Property<int>("ReportsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReportsId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportsId"));

                    b.Property<string>("AuthorEmail")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorEmail");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AuthorName");

                    b.Property<string>("AuthorPhoneNumber")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("AuthorPhoneNumber");

                    b.Property<string>("BannerUrl")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("BannerUrl");

                    b.Property<string>("Category")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Category");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PublishDate");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Title");

                    b.Property<bool?>("UseBanner")
                        .HasColumnType("bit")
                        .HasColumnName("UseBanner");

                    b.Property<string>("UserProfileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserProfileId");

                    b.Property<string>("Visibility")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Visibility");

                    b.HasKey("ReportsId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("tbl_Reports", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateId"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateId");

                    b.ToTable("tbl_State", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("tbl_Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("tbl_UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("tbl_UserTokens", (string)null);
                });

            modelBuilder.Entity("ARCN.Domain.Entities.ApplicationRoleClaim", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ARCN.Domain.Entities.Blog", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("Blogs")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ARCN.Domain.Entities.Journals", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("Journals")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ARCN.Domain.Entities.NewsLetter", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("NewsLetters")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ARCN.Domain.Entities.Reports", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("Reports")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ARCN.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ARCN.Domain.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Journals");

                    b.Navigation("NewsLetters");

                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
