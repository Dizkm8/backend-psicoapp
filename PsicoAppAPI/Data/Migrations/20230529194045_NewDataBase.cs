using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsicoAppAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdministrator = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FirstLastName = table.Column<string>(type: "TEXT", nullable: true),
                    SecondLastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Phone = table.Column<int>(type: "INTEGER", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForumPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    PublishedOn = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumPosts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Specialists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SpecialityId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialityName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FirstLastName = table.Column<string>(type: "TEXT", nullable: true),
                    SecondLastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Phone = table.Column<int>(type: "INTEGER", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialists_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: true),
                    specialisttId = table.Column<string>(type: "TEXT", nullable: true),
                    specialistName = table.Column<string>(type: "TEXT", nullable: true),
                    SpecialistId = table.Column<string>(type: "TEXT", nullable: true),
                    AppointmentStatusName = table.Column<string>(type: "TEXT", nullable: true),
                    AppointmentStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                        column: x => x.AppointmentStatusId,
                        principalTable: "AppointmentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialistId = table.Column<string>(type: "TEXT", nullable: true),
                    SpecialistName = table.Column<string>(type: "TEXT", nullable: true),
                    ForumPostId = table.Column<int>(type: "INTEGER", nullable: false),
                    ForumPostTitle = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ForumPosts_ForumPostId",
                        column: x => x.ForumPostId,
                        principalTable: "ForumPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    OnPublished = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    SpecialistId = table.Column<string>(type: "TEXT", nullable: true),
                    SpecialistName = table.Column<string>(type: "TEXT", nullable: true),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedPosts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedPosts_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentStatusId",
                table: "Appointments",
                column: "AppointmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialistId",
                table: "Appointments",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ForumPostId",
                table: "Comments",
                column: "ForumPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SpecialistId",
                table: "Comments",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedPosts_ClientId",
                table: "FeedPosts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedPosts_SpecialistId",
                table: "FeedPosts",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_ClientId",
                table: "ForumPosts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_SpecialityId",
                table: "Specialists",
                column: "SpecialityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FeedPosts");

            migrationBuilder.DropTable(
                name: "AppointmentStatuses");

            migrationBuilder.DropTable(
                name: "ForumPosts");

            migrationBuilder.DropTable(
                name: "Specialists");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Specialities");
        }
    }
}
