using Microsoft.EntityFrameworkCore.Migrations;

namespace E_TiketsMovie.Migrations
{
    public partial class Updatemoive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                schema: "guide",
                table: "Movie",
                newName: "endDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "endDate",
                schema: "guide",
                table: "Movie",
                newName: "DateTime");
        }
    }
}
