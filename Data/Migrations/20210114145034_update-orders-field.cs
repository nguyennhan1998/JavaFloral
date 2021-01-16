using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class updateordersfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "telephone",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "message",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "telephone",
                table: "Orders");
        }
    }
}
