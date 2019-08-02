using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiMeasurement.Data.Migrations
{
    public partial class AddedMacMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MAC",
                table: "Measurements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MAC",
                table: "Measurements");
        }
    }
}
