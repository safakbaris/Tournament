using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.Infrastructure.Migrations
{
    public partial class ChangeLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Log",
                table: "GameLogs");

            migrationBuilder.AddColumn<int>(
                name: "PlayedNumber",
                table: "GameLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayedNumber",
                table: "GameLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameLogs");

            migrationBuilder.AddColumn<string>(
                name: "Log",
                table: "GameLogs",
                type: "text",
                nullable: true);
        }
    }
}
