using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.Data.Migrations
{
    /// <inheritdoc />
    public partial class initforeign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    License_num = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entry_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exit_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_payment = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_spots = table.Column<int>(type: "int", nullable: false),
                    Available_spots = table.Column<int>(type: "int", nullable: false),
                    Price_per_hour = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parkings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "spots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parking_id = table.Column<int>(type: "int", nullable: false),
                    Spot_number = table.Column<int>(type: "int", nullable: false),
                    Is_occupied = table.Column<bool>(type: "bit", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_spots_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_spots_CarId",
                table: "spots",
                column: "CarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parkings");

            migrationBuilder.DropTable(
                name: "spots");

            migrationBuilder.DropTable(
                name: "cars");
        }
    }
}
