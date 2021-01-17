using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class updatefieldorderanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivedDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnswerTime",
                table: "Answers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AnswerTime",
                table: "Answers");
        }
    }
}
