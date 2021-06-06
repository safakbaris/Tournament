using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.Infrastructure.Migrations
{
    public partial class ChangePlayerIdToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "TournamentPlayers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentPlayers_PlayerId",
                table: "TournamentPlayers",
                newName: "IX_TournamentPlayers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayers_Users_UserId",
                table: "TournamentPlayers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayers_Users_UserId",
                table: "TournamentPlayers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TournamentPlayers",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentPlayers_UserId",
                table: "TournamentPlayers",
                newName: "IX_TournamentPlayers_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayers_Users_PlayerId",
                table: "TournamentPlayers",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
