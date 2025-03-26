using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNewFiedInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActivate",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActivate",
                table: "Users");
        }
    }
}
