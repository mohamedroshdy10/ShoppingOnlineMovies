using Microsoft.EntityFrameworkCore.Migrations;

namespace E_TiketsMovie.Migrations
{
    public partial class gh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movies_Actor_ActorID1",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movies_Produsser_ActorID",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actor_Movies_ActorID1",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropColumn(
                name: "ActorID1",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movies_Actor_ActorID",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID",
                principalSchema: "guide",
                principalTable: "Actor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movies_Actor_ActorID",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.AddColumn<int>(
                name: "ActorID1",
                schema: "guide",
                table: "Actor_Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_Movies_ActorID1",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movies_Actor_ActorID1",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID1",
                principalSchema: "guide",
                principalTable: "Actor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movies_Produsser_ActorID",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID",
                principalSchema: "guide",
                principalTable: "Produsser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
