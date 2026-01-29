using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spots_cars_CarId",
                table: "spots");

            migrationBuilder.DropIndex(
                name: "IX_spots_CarId",
                table: "spots");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "spots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_spots_CarId",
                table: "spots",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_spots_cars_CarId",
                table: "spots",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spots_cars_CarId",
                table: "spots");

            migrationBuilder.DropIndex(
                name: "IX_spots_CarId",
                table: "spots");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "spots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_spots_CarId",
                table: "spots",
                column: "CarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_spots_cars_CarId",
                table: "spots",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
