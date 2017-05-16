using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EmployeesEvaluation.Repository;
using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.WEB.Migrations
{
    [DbContext(typeof(EmployeesEvaluationContext))]
    partial class EmployeesEvaluationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("UserType")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(3);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2017, 5, 13, 9, 37, 29, 310, DateTimeKind.Local));

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2017, 5, 13, 9, 37, 29, 311, DateTimeKind.Local));

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DepartmentManagerId");

                    b.Property<string>("Disclosure");

                    b.Property<string>("Introduction");

                    b.Property<int>("SeasonId");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentManagerId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.EvaluationAssigned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DepartmentManagerId");

                    b.Property<string>("EmployeeId");

                    b.Property<int>("EvaluationId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EvaluationId");

                    b.ToTable("EvaluationAssigned");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.EvaluationQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("EvaluationId");

                    b.Property<int>("QuestionId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId");

                    b.HasIndex("QuestionId");

                    b.ToTable("EvaluationQuestion");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.EvaluationResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DepartmentManagerId");

                    b.Property<string>("EmployeeId");

                    b.Property<int>("EvaluationId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EvaluationId");

                    b.ToTable("EvaluationResponses");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.LikertAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<int>("QuestionId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("LikertAnswer");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2017, 5, 13, 9, 37, 29, 317, DateTimeKind.Local));

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<int>("Limit");

                    b.Property<string>("OwnershipId");

                    b.Property<int>("QuestionType");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2017, 5, 13, 9, 37, 29, 317, DateTimeKind.Local));

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("OwnershipId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.QuestionAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<int?>("EvaluationResponseId");

                    b.Property<string>("FileName");

                    b.Property<int>("LikertAnswerId");

                    b.Property<string>("OpenEndedAnswer");

                    b.Property<int>("QuestionId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationResponseId");

                    b.HasIndex("LikertAnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionAnswer");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.UserRelation", b =>
                {
                    b.Property<string>("DepartmentManagerId");

                    b.Property<string>("EmployeeId");

                    b.HasKey("DepartmentManagerId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("UserRelation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.Evaluation", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser", "DepartmentManager")
                        .WithMany()
                        .HasForeignKey("DepartmentManagerId");

                    b.HasOne("EmployeesEvaluation.Core.Models.Season", "Season")
                        .WithMany("Evaluations")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.EvaluationAssigned", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser", "Employee")
                        .WithMany("EvaluationsAssigned")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("EmployeesEvaluation.Core.Models.Evaluation", "Evaluation")
                        .WithMany("EvaluationsAssigned")
                        .HasForeignKey("EvaluationId");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.EvaluationQuestion", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.Evaluation", "Evaluation")
                        .WithMany("EvaluationQuestions")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmployeesEvaluation.Core.Models.Question", "Question")
                        .WithMany("EvaluationQuestions")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.EvaluationResponse", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser", "Employee")
                        .WithMany("EvaluationResponses")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("EmployeesEvaluation.Core.Models.Evaluation", "Evaluation")
                        .WithMany("EvaluationResponses")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.LikertAnswer", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.Question", "Question")
                        .WithMany("LikertAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.Question", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser", "Ownership")
                        .WithMany()
                        .HasForeignKey("OwnershipId");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.QuestionAnswer", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.EvaluationResponse", "EvaluationResponse")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("EvaluationResponseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmployeesEvaluation.Core.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("EmployeesEvaluation.Core.Models.UserRelation", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser", "DepartmentManager")
                        .WithMany("DepartmentManagersRelated")
                        .HasForeignKey("DepartmentManagerId");

                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser", "Employee")
                        .WithMany("EmployeesRelated")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("EmployeesEvaluation.Core.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });
        }
    }
}
