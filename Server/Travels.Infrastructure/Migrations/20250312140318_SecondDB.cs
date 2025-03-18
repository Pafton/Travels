using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travels.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelOfferHotel_Hotels_HotelsId",
                table: "TravelOfferHotel");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelOfferHotel_TravelOffers_TravelOffersId",
                table: "TravelOfferHotel");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelOfferTransport_Transports_TransportsId",
                table: "TravelOfferTransport");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelOfferTransport_TravelOffers_TravelOffersId",
                table: "TravelOfferTransport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelOfferTransport",
                table: "TravelOfferTransport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelOfferHotel",
                table: "TravelOfferHotel");

            migrationBuilder.RenameTable(
                name: "TravelOfferTransport",
                newName: "TransportTravelOffer");

            migrationBuilder.RenameTable(
                name: "TravelOfferHotel",
                newName: "HotelTravelOffer");

            migrationBuilder.RenameIndex(
                name: "IX_TravelOfferTransport_TravelOffersId",
                table: "TransportTravelOffer",
                newName: "IX_TransportTravelOffer_TravelOffersId");

            migrationBuilder.RenameIndex(
                name: "IX_TravelOfferHotel_TravelOffersId",
                table: "HotelTravelOffer",
                newName: "IX_HotelTravelOffer_TravelOffersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransportTravelOffer",
                table: "TransportTravelOffer",
                columns: new[] { "TransportsId", "TravelOffersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelTravelOffer",
                table: "HotelTravelOffer",
                columns: new[] { "HotelsId", "TravelOffersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelTravelOffer_Hotels_HotelsId",
                table: "HotelTravelOffer",
                column: "HotelsId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelTravelOffer_TravelOffers_TravelOffersId",
                table: "HotelTravelOffer",
                column: "TravelOffersId",
                principalTable: "TravelOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportTravelOffer_Transports_TransportsId",
                table: "TransportTravelOffer",
                column: "TransportsId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportTravelOffer_TravelOffers_TravelOffersId",
                table: "TransportTravelOffer",
                column: "TravelOffersId",
                principalTable: "TravelOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelTravelOffer_Hotels_HotelsId",
                table: "HotelTravelOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelTravelOffer_TravelOffers_TravelOffersId",
                table: "HotelTravelOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportTravelOffer_Transports_TransportsId",
                table: "TransportTravelOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportTravelOffer_TravelOffers_TravelOffersId",
                table: "TransportTravelOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransportTravelOffer",
                table: "TransportTravelOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelTravelOffer",
                table: "HotelTravelOffer");

            migrationBuilder.RenameTable(
                name: "TransportTravelOffer",
                newName: "TravelOfferTransport");

            migrationBuilder.RenameTable(
                name: "HotelTravelOffer",
                newName: "TravelOfferHotel");

            migrationBuilder.RenameIndex(
                name: "IX_TransportTravelOffer_TravelOffersId",
                table: "TravelOfferTransport",
                newName: "IX_TravelOfferTransport_TravelOffersId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelTravelOffer_TravelOffersId",
                table: "TravelOfferHotel",
                newName: "IX_TravelOfferHotel_TravelOffersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelOfferTransport",
                table: "TravelOfferTransport",
                columns: new[] { "TransportsId", "TravelOffersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelOfferHotel",
                table: "TravelOfferHotel",
                columns: new[] { "HotelsId", "TravelOffersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TravelOfferHotel_Hotels_HotelsId",
                table: "TravelOfferHotel",
                column: "HotelsId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelOfferHotel_TravelOffers_TravelOffersId",
                table: "TravelOfferHotel",
                column: "TravelOffersId",
                principalTable: "TravelOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelOfferTransport_Transports_TransportsId",
                table: "TravelOfferTransport",
                column: "TransportsId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelOfferTransport_TravelOffers_TravelOffersId",
                table: "TravelOfferTransport",
                column: "TravelOffersId",
                principalTable: "TravelOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
