using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.Infrastructure.Migrations
{
    public partial class AddDeleteBehaviours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameLogs_Games_GameId",
                table: "GameLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Tournaments_TournamentId",
                table: "TournamentPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentPlayers_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayers_PlayerId",
                table: "TournamentPlayers",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameLogs_Games_GameId",
                table: "GameLogs",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameLogs_Games_GameId",
                table: "GameLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Tournaments_TournamentId",
                table: "TournamentPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentPlayers_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayers_PlayerId",
                table: "TournamentPlayers",
                column: "PlayerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameLogs_Games_GameId",
                table: "GameLogs",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
