using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitOrFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActivate",
                table: "Users",
                newName: "IsActivate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActivate",
                table: "Users",
                newName: "isActivate");
        }
    }
}
