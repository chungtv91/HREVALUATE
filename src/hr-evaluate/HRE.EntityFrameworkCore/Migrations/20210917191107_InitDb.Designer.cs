﻿// <auto-generated />
using System;
using HRE.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRE.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(HrDbContext))]
    [Migration("20210917191107_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.1.21452.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HRE.Core.Entities.BOD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int?>("LevelID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("PositionID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("LevelID");

                    b.HasIndex("PositionID");

                    b.ToTable("BODs");
                });

            modelBuilder.Entity("HRE.Core.Entities.BodMemo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BodId")
                        .HasColumnType("int");

                    b.Property<string>("Cons")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<string>("Improve")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("MonthOfMemo")
                        .HasColumnType("int");

                    b.Property<string>("Pros")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("updatedate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BodId");

                    b.HasIndex("EmpId");

                    b.ToTable("BodMemoes");
                });

            modelBuilder.Entity("HRE.Core.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRE.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("CurrentLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentPositionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEvaluate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int?>("IsEnable")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int?>("NextLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("NextPositionId")
                        .HasColumnType("int");

                    b.Property<string>("Targets")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentLevelId");

                    b.HasIndex("CurrentPositionId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRE.Core.Entities.EvaluateYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("FromYear")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NextEvaluatePeriod")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ToYear")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EvaluateYears");
                });

            modelBuilder.Entity("HRE.Core.Entities.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("HRE.Core.Entities.ListEvaluated", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Img")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<string>("LevelName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<string>("PositionName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ListEvaluateds");
                });

            modelBuilder.Entity("HRE.Core.Entities.ManagerEmp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BODID")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int?>("EvaluateYearId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FirstPeriodEvaluate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SecondPeriodEvaluate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThirdPeriodEvaluate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BODID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("EvaluateYearId");

                    b.ToTable("ManagerEmps");
                });

            modelBuilder.Entity("HRE.Core.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("HRE.Core.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionName")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("HRE.Core.Entities.Sumary", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("AnswerName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("BodID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CurrentLevelID")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentPositionId")
                        .HasColumnType("int");

                    b.Property<int?>("EmpID")
                        .HasColumnType("int");

                    b.Property<int?>("Evaluatetimes")
                        .HasColumnType("int");

                    b.Property<int?>("NextLevelID")
                        .HasColumnType("int");

                    b.Property<int?>("NextPositionId")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionID")
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<int?>("TotalScore")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("BodID");

                    b.HasIndex("CurrentLevelID");

                    b.HasIndex("CurrentPositionId");

                    b.HasIndex("EmpID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Sumaries");
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorUserId")
                        .HasColumnType("int");

                    b.Property<int?>("DeleterUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastModifierUserId")
                        .HasColumnType("int");

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

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("BODID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorUserId")
                        .HasColumnType("int");

                    b.Property<int?>("DeleterUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastModifierUserId")
                        .HasColumnType("int");

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

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BODID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HRE.Core.Entities.BOD", b =>
                {
                    b.HasOne("HRE.Core.Entities.Department", "Department")
                        .WithMany("BODs")
                        .HasForeignKey("DepartmentID");

                    b.HasOne("HRE.Core.Entities.Level", "Level")
                        .WithMany("BODs")
                        .HasForeignKey("LevelID");

                    b.HasOne("HRE.Core.Entities.Position", "Position")
                        .WithMany("BODs")
                        .HasForeignKey("PositionID");

                    b.Navigation("Department");

                    b.Navigation("Level");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("HRE.Core.Entities.BodMemo", b =>
                {
                    b.HasOne("HRE.Core.Entities.BOD", "BOD")
                        .WithMany()
                        .HasForeignKey("BodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRE.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BOD");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRE.Core.Entities.Employee", b =>
                {
                    b.HasOne("HRE.Core.Entities.Level", "Level")
                        .WithMany("Employees")
                        .HasForeignKey("CurrentLevelId");

                    b.HasOne("HRE.Core.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("CurrentPositionId");

                    b.HasOne("HRE.Core.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");

                    b.Navigation("Level");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("HRE.Core.Entities.ManagerEmp", b =>
                {
                    b.HasOne("HRE.Core.Entities.BOD", "BOD")
                        .WithMany("ManagerEmps")
                        .HasForeignKey("BODID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRE.Core.Entities.Employee", "Employee")
                        .WithMany("ManagerEmps")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRE.Core.Entities.EvaluateYear", "EvaluateYear")
                        .WithMany("ManagerEmps")
                        .HasForeignKey("EvaluateYearId");

                    b.Navigation("BOD");

                    b.Navigation("Employee");

                    b.Navigation("EvaluateYear");
                });

            modelBuilder.Entity("HRE.Core.Entities.Question", b =>
                {
                    b.HasOne("HRE.Core.Entities.Level", "Level")
                        .WithMany("Questions")
                        .HasForeignKey("LevelId");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("HRE.Core.Entities.Sumary", b =>
                {
                    b.HasOne("HRE.Core.Entities.BOD", "BOD")
                        .WithMany("Sumaries")
                        .HasForeignKey("BodID");

                    b.HasOne("HRE.Core.Entities.Level", "Level")
                        .WithMany()
                        .HasForeignKey("CurrentLevelID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HRE.Core.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("CurrentPositionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HRE.Core.Entities.Employee", "Employee")
                        .WithMany("Sumaries")
                        .HasForeignKey("EmpID");

                    b.HasOne("HRE.Core.Entities.Question", "Question")
                        .WithMany("Sumaries")
                        .HasForeignKey("QuestionID");

                    b.Navigation("BOD");

                    b.Navigation("Employee");

                    b.Navigation("Level");

                    b.Navigation("Position");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomRoleClaim", b =>
                {
                    b.HasOne("HRE.Core.Identity.CustomRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUser", b =>
                {
                    b.HasOne("HRE.Core.Entities.BOD", "BOD")
                        .WithMany("Logins")
                        .HasForeignKey("BODID");

                    b.Navigation("BOD");
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserClaim", b =>
                {
                    b.HasOne("HRE.Core.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserLogin", b =>
                {
                    b.HasOne("HRE.Core.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserRole", b =>
                {
                    b.HasOne("HRE.Core.Identity.CustomRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRE.Core.Identity.CustomUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HRE.Core.Identity.CustomUserToken", b =>
                {
                    b.HasOne("HRE.Core.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRE.Core.Entities.BOD", b =>
                {
                    b.Navigation("Logins");

                    b.Navigation("ManagerEmps");

                    b.Navigation("Sumaries");
                });

            modelBuilder.Entity("HRE.Core.Entities.Department", b =>
                {
                    b.Navigation("BODs");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HRE.Core.Entities.Employee", b =>
                {
                    b.Navigation("ManagerEmps");

                    b.Navigation("Sumaries");
                });

            modelBuilder.Entity("HRE.Core.Entities.EvaluateYear", b =>
                {
                    b.Navigation("ManagerEmps");
                });

            modelBuilder.Entity("HRE.Core.Entities.Level", b =>
                {
                    b.Navigation("BODs");

                    b.Navigation("Employees");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("HRE.Core.Entities.Position", b =>
                {
                    b.Navigation("BODs");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HRE.Core.Entities.Question", b =>
                {
                    b.Navigation("Sumaries");
                });
#pragma warning restore 612, 618
        }
    }
}
