using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsicoAppAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Rut",
                table: "Users",
                newName: "Discriminator");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdministrator",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityName",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ForumPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "ForumPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "FeedPosts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialistId",
                table: "FeedPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SpecialistName",
                table: "FeedPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecialistId",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Appointments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "specialistName",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "specialisttId",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialistId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_Comments_Users_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecialityId",
                table: "Users",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_ClientId",
                table: "ForumPosts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedPosts_ClientId",
                table: "FeedPosts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedPosts_SpecialistId",
                table: "FeedPosts",
                column: "SpecialistId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_ClientId",
                table: "Appointments",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_SpecialistId",
                table: "Appointments",
                column: "SpecialistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPosts_Users_ClientId",
                table: "FeedPosts",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPosts_Users_SpecialistId",
                table: "FeedPosts",
                column: "SpecialistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Users_ClientId",
                table: "ForumPosts",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_specialities_SpecialityId",
                table: "Users",
                column: "SpecialityId",
                principalTable: "specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_ClientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_SpecialistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedPosts_Users_ClientId",
                table: "FeedPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedPosts_Users_SpecialistId",
                table: "FeedPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Users_ClientId",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_specialities_SpecialityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "specialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SpecialityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ForumPosts_ClientId",
                table: "ForumPosts");

            migrationBuilder.DropIndex(
                name: "IX_FeedPosts_ClientId",
                table: "FeedPosts");

            migrationBuilder.DropIndex(
                name: "IX_FeedPosts_SpecialistId",
                table: "FeedPosts");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SpecialistId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsAdministrator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpecialityName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ForumPosts");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "ForumPosts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "FeedPosts");

            migrationBuilder.DropColumn(
                name: "SpecialistId",
                table: "FeedPosts");

            migrationBuilder.DropColumn(
                name: "SpecialistName",
                table: "FeedPosts");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "specialistName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "specialisttId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Users",
                newName: "Rut");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "SpecialistId",
                table: "Appointments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Appointments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Rut");
        }
    }
}
