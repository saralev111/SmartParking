using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.Data.Migrations
{
    /// <inheritdoc />
    public partial class initforeign2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "spots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_spots_ParkingId",
                table: "spots",
                column: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_spots_parkings_ParkingId",
                table: "spots",
                column: "ParkingId",
                principalTable: "parkings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spots_parkings_ParkingId",
                table: "spots");

            migrationBuilder.DropIndex(
                name: "IX_spots_ParkingId",
                table: "spots");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "spots");
        }
    }
}
