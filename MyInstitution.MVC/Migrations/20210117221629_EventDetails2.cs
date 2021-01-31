using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyInstitution.MVC.Migrations
{
    public partial class EventDetails2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "EventDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "EventDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "EventDetails");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "EventDetails");
        }
    }
}
