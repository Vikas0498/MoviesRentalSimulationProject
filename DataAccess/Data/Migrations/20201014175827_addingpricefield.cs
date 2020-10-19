using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesRentalSimulationProject.Data.Migrations
{
    public partial class addingpricefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movies");
        }
    }
}
