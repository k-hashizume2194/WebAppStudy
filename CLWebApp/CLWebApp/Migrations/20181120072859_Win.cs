using Microsoft.EntityFrameworkCore.Migrations;

namespace CLWebApp.Migrations
{
    public partial class Win : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VictoryNum",
                table: "WinningRecords",
                newName: "Victory");

            migrationBuilder.RenameColumn(
                name: "DrawNum",
                table: "WinningRecords",
                newName: "Draw");

            migrationBuilder.RenameColumn(
                name: "DefeatNum",
                table: "WinningRecords",
                newName: "Defeat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Victory",
                table: "WinningRecords",
                newName: "VictoryNum");

            migrationBuilder.RenameColumn(
                name: "Draw",
                table: "WinningRecords",
                newName: "DrawNum");

            migrationBuilder.RenameColumn(
                name: "Defeat",
                table: "WinningRecords",
                newName: "DefeatNum");
        }
    }
}
