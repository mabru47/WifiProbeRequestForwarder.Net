using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiMeasurement.Data.Migrations
{
    public partial class AddedWifiChannelToMeasurementMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Channel",
                table: "Measurements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Measurements");
        }
    }
}
