using Microsoft.EntityFrameworkCore.Migrations;

namespace E_TiketsMovie.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movies_Actors_ActorID",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropTable(
                name: "Actors",
                schema: "guide");

            migrationBuilder.AddColumn<int>(
                name: "ActorID1",
                schema: "guide",
                table: "Actor_Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Actor",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.ID);
                });

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movies_Produsser_ActorID",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID",
                principalSchema: "guide",
                principalTable: "Produsser",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movies_Actor_ActorID1",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movies_Produsser_ActorID",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropTable(
                name: "Actor",
                schema: "guide");

            migrationBuilder.DropIndex(
                name: "IX_Actor_Movies_ActorID1",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.DropColumn(
                name: "ActorID1",
                schema: "guide",
                table: "Actor_Movies");

            migrationBuilder.CreateTable(
                name: "Actors",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movies_Actors_ActorID",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID",
                principalSchema: "guide",
                principalTable: "Actors",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
