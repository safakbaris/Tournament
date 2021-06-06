using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournament.Infrastructure.Migrations
{
    public partial class AddGameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastPlayedUserId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPlayedUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Games");
        }
    }
}
