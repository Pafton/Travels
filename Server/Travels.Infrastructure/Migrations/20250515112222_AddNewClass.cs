using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelOfferImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelOfferImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelOfferImages_TravelOffers_TravelOfferId",
                        column: x => x.TravelOfferId,
                        principalTable: "TravelOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelOfferImages_TravelOfferId",
                table: "TravelOfferImages",
                column: "TravelOfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelOfferImages");
        }
    }
}
