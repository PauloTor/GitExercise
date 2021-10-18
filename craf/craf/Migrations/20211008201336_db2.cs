using Microsoft.EntityFrameworkCore.Migrations;

namespace craf.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Search",
                newName: "Region");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Search",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Search");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Search",
                newName: "Adress");
        }
    }
}
