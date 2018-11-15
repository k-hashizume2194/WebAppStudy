using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CLWebApp.Migrations
{
    public partial class BmiRecordAddBmiDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BmiDate",
                table: "BmiRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BmiDate",
                table: "BmiRecords");
        }
    }
}
