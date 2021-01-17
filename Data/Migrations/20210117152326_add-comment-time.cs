using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class addcommenttime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CommentTime",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentTime",
                table: "Comments");
        }
    }
}
