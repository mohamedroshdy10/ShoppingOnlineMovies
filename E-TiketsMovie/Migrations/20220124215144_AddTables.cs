using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_TiketsMovie.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "guide");
            migrationBuilder.CreateTable(
                name: "Actors",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cinema",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinema", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produsser",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produsser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nescripation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieCatogery = table.Column<int>(type: "int", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false),
                    ProdusserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movie_Cinema_CinemaId",
                        column: x => x.CinemaId,
                        principalSchema: "guide",
                        principalTable: "Cinema",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movie_Produsser_ProdusserId",
                        column: x => x.ProdusserId,
                        principalSchema: "guide",
                        principalTable: "Produsser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actor_Movies",
                schema: "guide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor_Movies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Actor_Movies_Actors_ActorID",
                        column: x => x.ActorID,
                        principalSchema: "guide",
                        principalTable: "Actors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actor_Movies_Movie_MovieID",
                        column: x => x.MovieID,
                        principalSchema: "guide",
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_Movies_ActorID",
                schema: "guide",
                table: "Actor_Movies",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_Actor_Movies_MovieID",
                schema: "guide",
                table: "Actor_Movies",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_CinemaId",
                schema: "guide",
                table: "Movie",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ProdusserId",
                schema: "guide",
                table: "Movie",
                column: "ProdusserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actor_Movies",
                schema: "guide");

            migrationBuilder.DropTable(
                name: "Actors",
                schema: "guide");

            migrationBuilder.DropTable(
                name: "Movie",
                schema: "guide");

            migrationBuilder.DropTable(
                name: "Cinema",
                schema: "guide");

            migrationBuilder.DropTable(
                name: "Produsser",
                schema: "guide");
        }
    }
}
