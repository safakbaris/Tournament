using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.Infrastructure.Migrations
{
    public partial class AddDeleteBehaivourForTournamentPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Tournaments_TournamentId",
                table: "TournamentPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayers_Tournaments_TournamentId",
                table: "TournamentPlayers",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Tournaments_TournamentId",
                table: "TournamentPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayers_Tournaments_TournamentId",
                table: "TournamentPlayers",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
