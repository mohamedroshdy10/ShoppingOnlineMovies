using Microsoft.EntityFrameworkCore.Migrations;

namespace E_TiketsMovie.Migrations
{
    public partial class newID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCardItems_Movie_MovieID",
                table: "ShoppingCardItems");

            migrationBuilder.AlterColumn<int>(
                name: "MovieID",
                table: "ShoppingCardItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCardItems_Movie_MovieID",
                table: "ShoppingCardItems",
                column: "MovieID",
                principalSchema: "guide",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCardItems_Movie_MovieID",
                table: "ShoppingCardItems");

            migrationBuilder.AlterColumn<int>(
                name: "MovieID",
                table: "ShoppingCardItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCardItems_Movie_MovieID",
                table: "ShoppingCardItems",
                column: "MovieID",
                principalSchema: "guide",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
