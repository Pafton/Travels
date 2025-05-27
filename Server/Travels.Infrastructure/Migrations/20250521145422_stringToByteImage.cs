using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class stringToByteImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "TravelOfferImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "TravelOfferImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "TravelOfferImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TravelOfferImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
