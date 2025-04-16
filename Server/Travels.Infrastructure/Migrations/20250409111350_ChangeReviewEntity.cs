using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReviewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotLogginUser",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotLogginUser",
                table: "Reviews");
        }
    }
}
