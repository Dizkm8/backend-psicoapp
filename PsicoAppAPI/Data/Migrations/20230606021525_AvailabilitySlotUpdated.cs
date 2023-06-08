using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsicoAppAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AvailabilitySlotUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "AvailabilitySlots",
                newName: "IsAvailableOverride");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailableOverride",
                table: "AvailabilitySlots",
                newName: "IsAvailable");
        }
    }
}
