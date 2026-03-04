using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.Data.Migrations
{
    /// <inheritdoc />
    public partial class deleteParkingId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parking_id",
                table: "spots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Parking_id",
                table: "spots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
